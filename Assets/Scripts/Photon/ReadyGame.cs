using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ReadyGame : MonoBehaviour
{
    private static GameObject hostGameBoard;
    private static GameObject clientGameBoard;

    private GameObject playerRole;
    private GameObject oppositePlayerRole;
    [SerializeField] private Ship[] ships;

    private readonly Dictionary<bool, GameObject> PlayerRoleInfo = new Dictionary<bool, GameObject>()
    {
        {true, hostGameBoard},
        {false, clientGameBoard}
    };

    private void Awake()
    {
        bool isHost = PhotonNetwork.IsMasterClient;
        hostGameBoard = GameObject.FindWithTag("HostGameBoard");
        clientGameBoard = GameObject.FindWithTag("ClientGameBoard");
        
        playerRole = PlayerRoleInfo[isHost];
        oppositePlayerRole = PlayerRoleInfo[!isHost];
    }

    public void OnGameStart()
    {
        if (ValidatePlacedShips())
            PrepGame();
        else
            return;
    }

    private void PrepGame()
     {
         ships = oppositePlayerRole.transform.GetComponentsInChildren<Ship>();
         foreach (Ship ship in ships)
         {
             Destroy(ship.gameObject);
         }
         oppositePlayerRole.SetActive(true);
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
    
}
