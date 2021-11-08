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
        
        playerRole = PlayerRoleInfo(isHost);
        oppositePlayerRole = PlayerRoleInfo(!isHost);

        DisableRequiredGameBoard(oppositePlayerRole);
    }
    
    public void OnGameStart()
    {
        if (ValidatePlacedShips())
        {
            if (isHost)
                SyncHostCells.Invoke();
            else
                SyncClientCells.Invoke();
            
            if (HostGridManager.isHostReady && GuestGridManager.isClientReady)
                PrepGame();
            else
            {
                Debug.Log(HostGridManager.isHostReady);
                Debug.Log(GuestGridManager.isClientReady);
            }
        }

    }
    
    private void DisableRequiredGameBoard(GameObject gameBoard)
    {
        gameBoard.SetActive(false);
    }
    
    private void PrepGame()
     {
         for (int i = 0; i < 7; i++)
         {
             Destroy(oppositePlayerRole.transform.GetChild(i).gameObject);
         }

         oppositePlayerRole.SetActive(true);
         playerRole.SetActive(true);


         
         oppositePlayerRole.SetActive(true);
         playerRole.SetActive(false);
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
