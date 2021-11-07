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
    private void SendClientCells(Cell[,] receivedCells)
    {
        cells = receivedCells;
    }
    
    [PunRPC]
    private void SetClientReady()
    {
        isClientReady = true;
    }
}
