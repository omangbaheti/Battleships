using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class ReadyGame : MonoBehaviour
{
    public bool isHost;
    
    [SerializeField] private Ship[] ships;
    [SerializeField] private UnityEvent SyncHostCells;
    [SerializeField] private UnityEvent SyncClientCells;
    [SerializeField] private UnityEvent SetHostReady;
    [SerializeField] private UnityEvent SetClientReady;
    
    private GameObject hostGameBoard;
    private GameObject clientGameBoard;
    private GameObject playerRole;
    private GameObject oppositePlayerRole;
    private Cell[,] receiveCellsInfo;
    private void Awake()
    {
        isHost = PhotonNetwork.IsMasterClient;
        hostGameBoard = GameObject.FindWithTag("HostGameBoard");
        clientGameBoard = GameObject.FindWithTag("ClientGameBoard");

    }

    private void Start()
    {
        playerRole = PlayerRoleInfo(isHost);
        oppositePlayerRole = PlayerRoleInfo(!isHost);
        SetBoardChildrenActive(oppositePlayerRole, false);
    }

    public void OnGameStart()
    {
        if (ValidatePlacedShips())
        {
            if (isHost)
            {
                SyncHostCells.Invoke();
                SetHostReady.Invoke();
            }
            else
            {
                SyncClientCells.Invoke();
                SetClientReady.Invoke();
            }
            if (HostGridManager.isHostReady && GuestGridManager.isClientReady)
                PrepGame();
        }

    }
    
    private void SetBoardChildrenActive(GameObject gameBoard, bool activate)
    {
        foreach (Transform transform in gameBoard.transform)
        {
            transform.gameObject.SetActive(activate);
        }
    }
    
    private void PrepGame()
     {
         for (int i = 0; i < 7; i++)
         {
             if(oppositePlayerRole.transform.GetChild(i).gameObject.CompareTag("Destructibles"))
                Destroy(oppositePlayerRole.transform.GetChild(i).gameObject);
         }

         Cell.OnGameReady.Invoke();
         SetBoardChildrenActive(oppositePlayerRole, true);
         SetBoardChildrenActive(playerRole, false);
         PhotonView photonView = oppositePlayerRole.GetPhotonView();
         photonView.RequestOwnership();
     }
     
     private bool ValidatePlacedShips()
     {
         ships = playerRole.transform.GetComponentsInChildren<Ship>();
         foreach (Ship ship in ships)
         {
             if (ship.placedPosition == Vector2Int.zero)
                 return false;
         }
         return true;
     }

     private GameObject PlayerRoleInfo(bool isHost)
     {
         if (isHost)
             return hostGameBoard;
         return clientGameBoard;

     }
   
}
