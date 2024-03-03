using System;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Networking.Transport;

public class NetMessage
{
    public OpCode Code { get;set;}
    public virtual void Serialize(ref DataStreamWriter writer){
        writer.WriteByte((Byte)Code);
    }

    public virtual void Deserialize(DataStreamReader reader){

    }

    public virtual void ReceivedOnClient(){

    }
    public virtual void ReceivedOnServer(NetworkConnection cnn){

    }
}
