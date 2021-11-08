using System;
using UnityEngine;
using UnityEngine.Events;

public class Cell: MonoBehaviour
{
    public Vector2Int CellCoords { get; set; }
    public ShipType shipTypeOccupancy;
    
    private GridManagerMonoBehaviour gridManager;
    private Action<Vector2Int> onCellClick;

    public void SetShip(Transform shipObject)
    {
        shipObject.position = transform.position;
    }
    private void Awake()
    {
        gridManager = GetComponentInParent<GridManagerMonoBehaviour>();
        shipTypeOccupancy = ShipType.NULL;
        onCellClick += gridManager.UpdateGrid; 
    }

    private void OnMouseDown()
    {
        onCellClick(CellCoords);
    }

    private void OnGameReady()
    {
        onCellClick -= gridManager.UpdateGrid;
        onCellClick += CheckCellOccupancy;
    }

    private void CheckCellOccupancy(Vector2Int coordinates)
    {
        Debug.Log("Click");
        if (shipTypeOccupancy == ShipType.NULL)
        {
            GetComponent<Renderer>().material = gridManager.Miss;
        }
        else
        {
            GetComponent<Renderer>().material = gridManager.Hit;
            gridManager.DestroyCell(coordinates);
        }
    }

}

public enum ShipType
{
    NULL=-1,
    Carrier = 0,
    Submarine = 1,
    Cruiser = 2,
    Battleship = 3,
    Destroyer = 4
}
