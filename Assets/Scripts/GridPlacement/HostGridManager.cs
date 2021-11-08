using System;
using Photon.Pun;


public class HostGridManager : GridManagerMonoBehaviour
{
    public static bool isHostReady = false;
    
    void Start()
    {
        oceanTiles = CreateBoard();
        ships = GetComponentsInChildren<Ship>();
    }

    public void SyncHostCells()
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
    
    public void SendHostReadySignal()
    {
        photonView.RPC("SetHostReady", RpcTarget.All);
    }
    
    [PunRPC]
    private void SendHostCells(bool isVertical, int placedPositionX, int placedPositionY, int shipType)
    {
        OnCellsReceived(isVertical, placedPositionX, placedPositionY, shipType);
    }

    [PunRPC]
    private void SetHostReady()
    {
        isHostReady = true;
    }
}
