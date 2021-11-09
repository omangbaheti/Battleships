using System;
using System.Collections;
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
        if ((GridManagerMonoBehaviour.isHostTurn && PhotonNetwork.IsMasterClient) || (!GridManagerMonoBehaviour.isHostTurn && !PhotonNetwork.IsMasterClient))
        {

            if (guessed)
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
            gridManager.photonView.RPC("UpdatingGuessedPositions", RpcTarget.Others, coordinates.x, coordinates.y);
            StartCoroutine(DelayedSwitchTurn());
            gridManager.DidIWin();
            guessed = true;
        }
    }
    
    private void OnDisable()
    {
        OnGameReady -= ChangeCellFunctionality;
    }

    private IEnumerator DelayedSwitchTurn()
    {
        yield return new WaitForSeconds(.5f);
        GridManagerMonoBehaviour.isHostTurn = !GridManagerMonoBehaviour.isHostTurn;
        TurnHandler.SwitchTurnAction(GridManagerMonoBehaviour.isHostTurn);
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
