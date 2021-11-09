using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class ReadyGame : PlayerBoardsHandler
{
    
    [SerializeField] private Ship[] ships;
    [SerializeField] private UnityEvent SyncHostCells;
    [SerializeField] private UnityEvent SyncClientCells;
    [SerializeField] private UnityEvent SetHostReady;
    [SerializeField] private UnityEvent SetClientReady;

    private void Start()
    {
        Initialize();
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
    
    private void PrepGame()
     {
         for (int i = 0; i < 7; i++)
         {
             if(oppositePlayerRole.transform.GetChild(i).gameObject.CompareTag("Destructibles"))
                Destroy(oppositePlayerRole.transform.GetChild(i).gameObject);
         }

         SetBoardChildrenActive(oppositePlayerRole, true);
         SetBoardChildrenActive(playerRole, false);
         Cell.OnGameReady.Invoke();
         PhotonView photonView = oppositePlayerRole.GetPhotonView();
         photonView.RequestOwnership();
     }
     

     
   
}
