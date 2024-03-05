using UnityEngine;

public class Preloader : MonoBehaviour
{
    private void Start(){
        Application.targetFrameRate = 60;
        string[] args = System.Environment.GetCommandLineArgs();
        for(int i = 0;i<args.Length;i++){
            if(args[i] == "-launch-as-server"){
                OnOnlineServerHostStart();
            }
        }
    }

    public void OnOnlineHostButton(){
        GameUI.Instance.SetLocalGame?.Invoke(false);
        Server.Instance.Init(8007);
        Client.Instance.Init("127.0.0.1",8007);
        GameUI.Instance.menuAnimator.SetTrigger("HostMenu");
    }

    public void OnOnlineServerHostStart(){
        GameUI.Instance.SetLocalGame?.Invoke(false);
        Server.Instance.Init(8007);
    }
}
