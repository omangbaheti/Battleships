using Photon.Pun;
using UnityEngine.Experimental.GlobalIllumination;


public class HostGridManager : GridManagerMonoBehaviour
{
    public static bool isHostReady = false; 
    void Start()
    {
        oceanTiles = CreateBoard();
    }

    public void SyncHostCells()
    {
        photonView.RPC("SendHostCells", RpcTarget.Others, cells);
    }
    
    public void SendHostReadySignal()
    {
        photonView.RPC("SetClientReady", RpcTarget.Others);
    }
    
    [PunRPC]
    void SendHostCells(Cell[,] receivedCells)
    {
        cells = receivedCells;
    }

    [PunRPC]
    void SetHostReady()
    {
        isHostReady = true;
    }
}
