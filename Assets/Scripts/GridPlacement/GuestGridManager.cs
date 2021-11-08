using Photon.Pun;
using UnityEngine;

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
            photonView.RPC("SendClientCells", RpcTarget.Others,isVertical, placedPositionX, placedPositionY, shipType);
        }
    }

    public void SendClientReadySignal()
    {
        photonView.RPC("SetClientReady", RpcTarget.All);
    }
    
    [PunRPC]
    private void SendClientCells(bool isVertical, int placedPositionX, int placedPositionY, int shipType)
    {
        int length = Ship.shipLengthInfo[(ShipType)shipType];
        Vector2Int orientation = Ship.orientationInfo[isVertical];
        for (int i = 0; i < length; i++) 
        {
            cells[placedPositionX + i * orientation.x, placedPositionY + i * orientation.y].shipTypeOccupancy = (ShipType)shipType;
        }
    }
    
    [PunRPC]
    private void SetClientReady()
    {
        isClientReady = true;
    }
}
