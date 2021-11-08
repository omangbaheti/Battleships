using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ship : MonoBehaviour
{
    public Vector2Int placedPosition;
    public ShipType shipType;
    public bool isVertical = true;

    private Material[] allMaterials;
    private GridManagerMonoBehaviour gridManager;
    private List<Color> allColors = new List<Color>();
    
    public static Dictionary<bool, Vector2Int> orientationInfo = new Dictionary<bool, Vector2Int>()
    {
        { false, new Vector2Int(1, 0) },
        { true, new Vector2Int(0, 1) }
    };

    public static Dictionary<ShipType, int> shipLengthInfo = new Dictionary<ShipType, int>()
    {
        {ShipType.Cruiser, 3},
        {ShipType.Submarine, 3},
        {ShipType.Carrier, 5},
        {ShipType.Destroyer, 2},
        {ShipType.Battleship, 4}
    };
    
    private void Awake()
    {
        gridManager = GetComponentInParent<GridManagerMonoBehaviour>();
    }
    
    private void Start()
    {
        allMaterials = GetComponentInChildren<Renderer>().materials;
        for (int i = 0; i < allMaterials.Length; i++)
            allColors.Add(allMaterials[i].color);
    }

    private void OnMouseDown()
    {
        gridManager.CurrentShip = gameObject;
    }
    
    public void FlashColor(Color tempColor)
    {
        foreach(Material mat in allMaterials)
        {
            mat.color = tempColor;
        }
        Invoke("ResetColor", 0.5f);
    }

    private void ResetColor()
    {
        int i = 0; 
        foreach(Material mat in allMaterials)
        {
            mat.color = allColors[i++];
        }
    }
}

