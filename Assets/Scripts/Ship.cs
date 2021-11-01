using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GridManagerMonoBehaviour gridManager;
    public ShipType ship;
    public int length;
    public bool isVertical = true;

    private void Awake()
    {
        gridManager = GetComponentInParent<GridManagerMonoBehaviour>();
    }

    private Dictionary<ShipType, int> shipLengthInfo = new Dictionary<ShipType, int>()
    {
        {ShipType.Cruiser, 3},
        {ShipType.Submarine, 3},
        {ShipType.Carrier, 5},
        {ShipType.Destroyer, 2},
        {ShipType.Battleship, 4}
    };

    private void OnMouseDown()
    {
        gridManager.CurrentShip = gameObject;
        Debug.Log(gridManager.CurrentShip);
    }
}
