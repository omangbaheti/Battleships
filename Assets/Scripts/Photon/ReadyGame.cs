using Photon.Pun;
using UnityEngine;

public class ReadyGame : MonoBehaviour
{
    [SerializeField] private Ship[] ships;
    private bool isHostReady = false;
    private bool isClientReady = false;
    private GameObject hostGameBoard;
    private GameObject clientGameBoard;
    private GameObject playerRole;
    private GameObject oppositePlayerRole;
    
    
    

    private void Awake()
    {
        bool isHost = PhotonNetwork.IsMasterClient;
        hostGameBoard = GameObject.FindWithTag("HostGameBoard");
        clientGameBoard = GameObject.FindWithTag("ClientGameBoard");
        
        playerRole = PlayerRoleInfo(true);
        oppositePlayerRole = PlayerRoleInfo(false);

        DisableRequiredGameBoard(oppositePlayerRole);
    }

    private void DisableRequiredGameBoard(GameObject gameBoard)
    {
        gameBoard.SetActive(false);
    }

    public void OnGameStart()
    {
        if (ValidatePlacedShips() && isHostReady && isClientReady)
            PrepGame();
        else
            return;
    }

    private void PrepGame()
     {
         for (int i = 0; i < 7; i++)
         {
             Destroy(oppositePlayerRole.transform.GetChild(i).gameObject);
         }
         oppositePlayerRole.SetActive(true);
         playerRole.SetActive(false
         );
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
