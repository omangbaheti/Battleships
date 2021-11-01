using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedGuestChecks : MonoBehaviour
{
    /// <summary>
    /// This code base is super jank the only way we
    /// are going to implement multiplayer without
    /// disturbing this sleeping demon is by running
    /// all the check functions in this script on
    /// every opponent click. If that fails, god help us.
    /// </summary>

    public bool IsThisClickAHit { get; set; } = false;

    public bool IsThisClickSinkingShip { get; set; } = false;

    public bool IsThisClickResultingInTheGuestWinning { get; set; } = false;

    public bool CheckCurrentHit()
    {
        return IsThisClickAHit;
    }

    public bool CheckIfShipSunk()
    {
        return IsThisClickSinkingShip;
    }

    public bool DidGuestJustWin()
    {
        return IsThisClickResultingInTheGuestWinning;
    }
}
