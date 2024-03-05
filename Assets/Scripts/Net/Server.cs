using System;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public class Server : MonoBehaviour
{

    #region Singleton

    public static Server Instance {set;get;}

    private void Awake(){
        Instance = this;
        board = FindObjectOfType<Chessboard>();
    }
    #endregion
    public NetworkDriver driver;
    private NativeList<NetworkConnection> connections;
    Chessboard board;

    public bool isActive = false;
    private const float keepAliveTickRate = 20.0f;
    private float lastKeepAlive;

    public Action connectionDropped;

    //Methods
    public void Init(ushort port){
        board.isHost = true;
        driver = NetworkDriver.Create();
        
        NetworkEndpoint endpoint = NetworkEndpoint.AnyIpv4;
        endpoint.Port = port;


        if(driver.Bind(endpoint) != 0){
            Debug.Log("unable to bind" + endpoint.Port);
            return;
        }else{
            driver.Listen();
            Debug.Log("listening on port" + endpoint.Port);
        }

        connections = new NativeList<NetworkConnection>(2,Allocator.Persistent);
        isActive = true;
    }

    public void Shutdown(){
        if(isActive){
            driver.Dispose();
            connections.Dispose();
            isActive = false;
        }
    }

    public void OnDestroy() {
        Shutdown();
    }

    public void Update() {
        if(!isActive){
            return;
        }

        KeepAlive();

        driver.ScheduleUpdate().Complete();
        CleanupConnections();
        AcceptNewConnections();
        UpdateMessagePump();
    }
    private void KeepAlive(){
        if(Time.time - lastKeepAlive > keepAliveTickRate){
            lastKeepAlive = Time.time;
            Broadcast(new NetKeepAlive());
        }
    }

    private void CleanupConnections(){
        for(int i = 0; i<connections.Length;i++){
            if(!connections[i].IsCreated){
                connections.RemoveAtSwapBack(i);
                --i;
            }
        }
    }

    private void AcceptNewConnections(){
        NetworkConnection c;
        while((c = driver.Accept()) != default(NetworkConnection)){
            connections.Add(c);
        }
    }

    private void UpdateMessagePump(){
        DataStreamReader stream;
        for (int i = 0; i < connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while((cmd = driver.PopEventForConnection(connections[i],out stream)) != NetworkEvent.Type.Empty){
                if(cmd == NetworkEvent.Type.Data){
                    NetUtility.OnData(stream,connections[i],this);
                }
                else if(cmd == NetworkEvent.Type.Disconnect){
                    Debug.Log("Client disconnected from server");
                    connections[i] = default(NetworkConnection);
                    connectionDropped?.Invoke();
                    board.resetServerBoard();
                    //shutdown when other player disconnects and reset server when player disconnects to reset states
                }
            }
        }
    }

    void DisconnectAllClients()
    {
        // Iterate through the list and disconnect each client
        foreach (var connection in connections)
        {
            if (connection != null && connection != default(NetworkConnection))
            {
                // Disconnect the client
                connection.Disconnect(driver);
            }
        }
    }

    //Server

    public void SendToClient(NetworkConnection connection, NetMessage msg){
        DataStreamWriter writer;
        driver.BeginSend(connection,out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }
    public void Broadcast(NetMessage msg){
        for(int i = 0; i <connections.Length;i++){
            if(connections[i].IsCreated){
                SendToClient(connections[i],msg);
            }
        }
    }
}

