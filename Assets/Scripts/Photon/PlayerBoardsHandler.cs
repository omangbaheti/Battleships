using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerBoardsHandler : MonoBehaviour
{
    public bool isHost;
    protected GameObject hostGameBoard;
    protected GameObject clientGameBoard;
    protected GameObject playerRole;
    protected GameObject oppositePlayerRole;


    public void Initialize()
    {
        isHost = PhotonNetwork.IsMasterClient;
        hostGameBoard = GameObject.FindWithTag("HostGameBoard");
        clientGameBoard = GameObject.FindWithTag("ClientGameBoard");
        playerRole = PlayerRoleInfo(isHost);
        oppositePlayerRole = PlayerRoleInfo(!isHost);
        SetBoardChildrenActive(oppositePlayerRole, false);
    }
    
    protected void SetBoardChildrenActive(GameObject gameBoard, bool activate)
    {
        foreach (Transform childTransform in gameBoard.transform)
        {
            childTransform.gameObject.SetActive(activate);
        }
    }
    
    protected GameObject PlayerRoleInfo(bool host)
    {
        if (host)
            return hostGameBoard;
        return clientGameBoard;
    }
    
    protected bool ValidatePlacedShips()
    {
        return GridManagerMonoBehaviour.placedShips >= 5;
    }
}
