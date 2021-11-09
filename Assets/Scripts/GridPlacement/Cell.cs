using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class Cell: MonoBehaviour
{
    public Vector2Int CellCoords { get; set; }
    public ShipType shipTypeOccupancy;
    public static Action OnGameReady;
    
    private GridManagerMonoBehaviour gridManager;
    private Action<Vector2Int> onCellClick;
    private bool guessed;

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

    private void OnEnable()
    {
        OnGameReady += ChangeCellFunctionality;
        
    }

    private void OnMouseDown()
    {
        onCellClick(CellCoords);
    }

    private void ChangeCellFunctionality()
    {
        onCellClick -= gridManager.UpdateGrid;
        onCellClick += CheckCellOccupancy;
    }

    private void CheckCellOccupancy(Vector2Int coordinates)
    {
        if (!(GridManagerMonoBehaviour.isHostTurn && PhotonNetwork.IsMasterClient))
            return;
        if(guessed)
            return;
        if (shipTypeOccupancy == ShipType.NULL)
        {
            GetComponent<Renderer>().material = gridManager.Miss;
        }
        else
        {
            GetComponent<Renderer>().material = gridManager.Hit;
            gridManager.DestroyCell(coordinates);
        }
        guessed = true;
    }
    
    private void OnDisable()
    {
        OnGameReady -= ChangeCellFunctionality;
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
