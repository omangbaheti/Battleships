using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LaunchPhoton : MonoBehaviourPunCallbacks
{
    [SerializeField] private Image onlineStatus;
    
    private static LaunchPhoton _instance;
    
    public static LaunchPhoton Instance { get => _instance; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        
        Connect();
    }

    public void Connect()
    {
        if(PhotonNetwork.IsConnected)
            return;

        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Hello, Connected to Photon");
    }
}