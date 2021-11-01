using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GridPropertiesMonoBehaviour : MonoBehaviour
{
    public static int Width { get => width; }
    public static int Height { get => height; }
    public static int CellSize { get => cellSize; }
    
    public Cell[,] GridArray { 
        get => gridArray;
        set => gridArray = value;
    }
    
    
    private static int width = 10;
    private static int height = 10;
    private static int cellSize = 1;
    private Cell[,] gridArray = new Cell[width, height];

    public Vector3 GetWorldPosition(Vector2Int cellPosition)
    {
        return new Vector3(cellPosition.x,0,cellPosition.y) * cellSize;
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x, z;
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
        return new Vector2Int(x, z);
    }
}


