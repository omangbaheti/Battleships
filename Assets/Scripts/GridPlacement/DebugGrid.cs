using TMPro;
using UnityEngine;

[RequireComponent(typeof(GridManagerMonoBehaviour))]
public class DebugGrid : GridPropertiesMonoBehaviour
{
    private float duration = 100f;
    public bool canDebug;
    public GameObject textMeshPrefab;
    private TextMeshProUGUI[,] debugTextArrays = new TextMeshProUGUI[Width, Height]; 
    
    
    void Start()
    {
        RectTransform canvas = GetComponentInParent<RectTransform>();
        canvas.sizeDelta = new Vector2(CellSize * Width, CellSize *Height);
        if(!canDebug) return;

        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int z = 0; z < GridArray.GetLength(1); z++)
            {
                Vector2Int gridPosition = new Vector2Int(x, z);
                string gridPositionString = gridPosition.ToString();
                debugTextArrays[x, z] = CreateWorldText(transform, gridPositionString, GetWorldPosition(gridPosition));
                Vector3 startPosition = GetWorldPosition(gridPosition);
                Vector3 verticallyUpPosition = GetWorldPosition(gridPosition + Vector2Int.up);
                Vector3 horizontallyRightPosition = GetWorldPosition(gridPosition + Vector2Int.right);
                Debug.DrawLine(startPosition, verticallyUpPosition, Color.white, duration);
                Debug.DrawLine(startPosition, horizontallyRightPosition, Color.white, duration);
            }
        }
        
    }
    
    
    public TextMeshProUGUI CreateWorldText(Transform parent, string gridPosition, Vector3 localPosition)
    {
        GameObject textMeshPro = Instantiate(textMeshPrefab, parent);
        TextMeshProUGUI textMesh = textMeshPro.GetComponent<TextMeshProUGUI>();
        RectTransform tmpTransform = textMeshPro.GetComponent<RectTransform>();
        tmpTransform.sizeDelta = new Vector2(CellSize, CellSize);
        tmpTransform.localPosition = localPosition;
        textMesh.text = gridPosition;
        return textMesh;
    }

 
}
