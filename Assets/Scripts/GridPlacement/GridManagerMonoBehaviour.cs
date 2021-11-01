using System;   
using System.Collections.Generic;
using UnityEngine;

public class GridManagerMonoBehaviour : GridPropertiesMonoBehaviour
{ 
   [SerializeField] private GameObject tile;
   public GameObject CurrentShip;
   protected GameObject[,] tiles = new GameObject[Width, Height];
   protected Cell[,] cells = new Cell[Width, Height];
   public GameObject[,] Tiles { get=> tiles;}

   public void RotateShip()
   {
       Ship currentShip = CurrentShip.GetComponent<Ship>();
       Debug.Log(currentShip.ship);
       Quaternion shipRotation = CurrentShip.transform.rotation;
       Vector3 finalRotation = new Vector3(shipRotation.x, Convert.ToInt32(currentShip.isVertical) * 90, shipRotation.z);
       Quaternion finalRotationButItsAFuckingQuaternion=new Quaternion();
       finalRotationButItsAFuckingQuaternion.eulerAngles = finalRotation;
       CurrentShip.transform.rotation = finalRotationButItsAFuckingQuaternion;
       currentShip.isVertical = !currentShip.isVertical;
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
    
    
    //NOTE TO SELF: NEED TO CLEAN THIS PIECE OF SHIT
    public void UpdateGrid(Vector2Int coordinates)
    {
        Ship currentShip = CurrentShip.GetComponent<Ship>();
        if (ValidateGridCells(coordinates, currentShip))
        {
            if (currentShip.isVertical)
            {
                for (int i = 0; i < currentShip.length; i++)
                {
                    cells[coordinates.x + i, coordinates.y].shipTypeOccupancy = currentShip.ship;
                }
            }
            else 
            {
                for (int i = 0; i < currentShip.length; i++)
                {
                    cells[coordinates.x, coordinates.y+i].shipTypeOccupancy = currentShip.ship;
                }
            }
        }
    }



    protected bool ValidateGridCells(Vector2Int currentPosition, Ship currentShip)
    {
        if (currentShip.isVertical)
        {
            for (int i = 0; i < currentShip.length; i++)
            {
                if (cells[currentPosition.x + i, currentPosition.y].shipTypeOccupancy != ShipType.NULL)
                    return false;
            }
        }
        else
        {
            for (int i = 0; i < currentShip.length; i++)
            {
                if (cells[currentPosition.x, currentPosition.y+i].shipTypeOccupancy != ShipType.NULL)
                    return false;
            }
        }
        return true;
    }

}
