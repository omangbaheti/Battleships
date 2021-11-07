using System;   
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GridManagerMonoBehaviour : GridPropertiesMonoBehaviour
{ 
    public GameObject CurrentShip = null; 
    public Cell[,] cells = new Cell[Width, Height];
    public PhotonView photonView;
    
    protected GameObject[,] oceanTiles = new GameObject[Width, Height];

    public Material Hit;
    public Material Miss;
    
    
    [SerializeField] private GameObject tile; 
    
    public GameObject[,] Tiles { get=> oceanTiles;}

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void RotateShip()
    {
       Ship currentShip = CurrentShip.GetComponent<Ship>();
       Quaternion shipRotation = CurrentShip.transform.rotation;
       Vector3 finalRotation = new Vector3(shipRotation.x, Convert.ToInt32(currentShip.isVertical) * 90, shipRotation.z);
       currentShip.isVertical = !currentShip.isVertical;
       ClearPreviousPositions(currentShip.shipType);
       
       if (ValidateGridCells(currentShip.placedPosition, currentShip))
       {
           shipRotation.eulerAngles = finalRotation;
           CurrentShip.transform.rotation = shipRotation;
           UpdateGrid(currentShip.placedPosition);
       }
       else
       {
           currentShip.isVertical = !currentShip.isVertical;
           currentShip.FlashColor(Color.red);
           UpdateGrid(currentShip.placedPosition);
       }
       
    }

    public void UpdateGrid(Vector2Int coordinates)
    {
       if(CurrentShip == null)
           return;
       Ship currentShip = CurrentShip.GetComponent<Ship>();
       int length = currentShip.shipLengthInfo[currentShip.shipType];
       Vector2Int orientation = currentShip.orientationInfo[currentShip.isVertical];
       ClearPreviousPositions(currentShip.shipType);
       
       if (ValidateGridCells(coordinates, currentShip))
       {
           for (int i = 0; i < length; i++)
           {
               cells[coordinates.x + i * orientation.x, coordinates.y + i * orientation.y].shipTypeOccupancy = currentShip.shipType;
           }
           cells[coordinates.x, coordinates.y].SetShip(currentShip.transform);
           currentShip.placedPosition = coordinates;
       }
       else
       {
           currentShip.FlashColor(Color.red);
       }
    }

    public void DestroyCell(Vector2Int coordinates)
    {
        cells[coordinates.x, coordinates.y].shipTypeOccupancy = ShipType.NULL;
    }
   
    protected GameObject[,] CreateBoard()
    {
        GameObject[,] tiles = new GameObject[Width,Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector3 instantiatingPosition = new Vector3(Width * CellSize * i, 0f, Height * CellSize * j);
                tiles[i,j] = Instantiate(tile,instantiatingPosition, Quaternion.identity, transform);
                cells[i, j] = tiles[i, j].GetComponent<Cell>();
                cells[i,j].CellCoords = new Vector2Int(i, j);
            }
        }
        return tiles;
    }
    
    protected bool ValidateGridCells(Vector2Int currentPosition, Ship currentShip)
    {
        int length = currentShip.shipLengthInfo[currentShip.shipType];
        Vector2Int orientation = currentShip.orientationInfo[currentShip.isVertical];
        for (int i = 0; i < length; i++)
        {
            if (cells[currentPosition.x + i * orientation.x, currentPosition.y + i * orientation.y].shipTypeOccupancy != ShipType.NULL)
                return false;
        }
        return true;
    }

    protected void ClearPreviousPositions(ShipType shipType)
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (cells[i, j].shipTypeOccupancy == shipType)
                {
                    cells[i, j].shipTypeOccupancy = ShipType.NULL;
                }
            }
        }
    }

}
