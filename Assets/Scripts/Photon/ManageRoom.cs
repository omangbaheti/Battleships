using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using WebSocketSharp;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ManageRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoomField;
    public TMP_InputField joinRoomField;
    
    private static ManageRoom _instance;
    private string userName;
    
    public static ManageRoom Instance { get => _instance;  }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

    public void CreateRoom()
    {
        string roomName = createRoomField.text;
        if(roomName.IsNullOrEmpty())
            return;

        RoomOptions roomOptions = new RoomOptions() { IsOpen = true, IsVisible = true };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomField.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        StartCoroutine(LoadAsynchronously("BattleShips"));
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        StartCoroutine(LoadAsynchronously("BattleShips"));
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }
    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        
    }
    
    private IEnumerator LoadAsynchronously(string scene)
    {
        
        PhotonNetwork.LoadLevel(scene);

        yield return null;

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        
    }
}