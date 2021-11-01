using System;
using UnityEngine;
using UnityEngine.Events;

public class Cell: MonoBehaviour
{
    public Vector2Int CellCoords { get; set; }

    public ShipType shipTypeOccupancy = ShipType.NULL;
    private GridManagerMonoBehaviour gridManager;

    private void Awake()
    {
        gridManager = GetComponentInParent<GridManagerMonoBehaviour>();
    }

    private void OnMouseDown()
    {
        gridManager.UpdateGrid(CellCoords);
        SetShip(gridManager.CurrentShip.transform);
    }

    public void SetShip(Transform shipObject)
    {
        shipObject.position = transform.position;
    }

    public bool CanBuild()
    {
        if (shipTypeOccupancy == ShipType.NULL)
            return true;
        return false;
    }
    
}

public enum ShipType
{
    NULL=-1,
    Carrier,
    Submarine,
    Cruiser,
    Battleship,
    Destroyer
}
