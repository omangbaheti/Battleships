using Photon.Pun;

public class GuestGridManager : GridManagerMonoBehaviour
{
    
    public static bool isClientReady = false; 
    void Start()
    {
        oceanTiles = CreateBoard();
        ships = GetComponentsInChildren<Ship>();
    }
    
    public void SyncClientCells()
    {
        foreach (Ship ship in ships)
        {
            bool isVertical = ship.isVertical;
            int placedPositionX = ship.placedPosition.x;
            int placedPositionY = ship.placedPosition.y;
            int shipType = (int)ship.shipType;
            photonView.RPC("SendHostCells", RpcTarget.Others,isVertical, placedPositionX, placedPositionY, shipType);
        }
    }

    public void SendClientReadySignal()
    {
        photonView.RPC("SetClientReady", RpcTarget.Others);
    }
    
    [PunRPC]
    private void SendClientCells(bool isVertical, int placedPositionX, int placedPositionY, int shipType)
    {
        OnCellsReceived(isVertical, placedPositionX, placedPositionY, shipType);
    }
    
    [PunRPC]
    private void SetClientReady()
    {
        isClientReady = true;
    }
}
