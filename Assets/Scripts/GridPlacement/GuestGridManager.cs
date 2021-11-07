using Photon.Pun;

public class GuestGridManager : GridManagerMonoBehaviour
{
    
    public static bool isClientReady = false; 
    void Start()
    {
        oceanTiles = CreateBoard();
    }
    
    public void SyncClientCells()
    {
        photonView.RPC("SendClientCells", RpcTarget.Others, cells);
    }

    public void SendClientReadySignal()
    {
        photonView.RPC("SetClientReady", RpcTarget.Others);
    }
    
    [PunRPC]
    void SendClientCells(Cell[,] receivedCells)
    {
        cells = receivedCells;
    }
    
    [PunRPC]
    void SetClientReady()
    {
        isClientReady = true;
    }
}
