using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class TurnHandler : PlayerBoardsHandler
{

    public static Action<bool> SwitchTurnAction;
    private PhotonView hostPhotonView;
    private PhotonView clientPhotonView;

    private void Start()
    {
        Initialize();
        hostPhotonView = hostGameBoard.GetPhotonView();
        clientPhotonView = clientGameBoard.GetPhotonView();
    }

    private void OnEnable()
    {
        SwitchTurnAction += SwitchTurn;
    }

    private void SwitchTurn(bool myTurn)
    {
        hostPhotonView.RPC("SwitchHostTurn", RpcTarget.All, myTurn);
        clientPhotonView.RPC("SwitchClientTurn", RpcTarget.All, myTurn);
    }
    

    private void OnDisable()
    {
        SwitchTurnAction -= SwitchTurn;
    }
}
