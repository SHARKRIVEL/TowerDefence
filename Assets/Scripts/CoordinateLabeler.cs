using UnityEngine;
using TMPro;

public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro coordsText;
    Vector2Int coordinates;
    GridManager gridManager;

    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color pathColor = Color.blue;
    [SerializeField] Color explorideColor = Color.yellow;
    [SerializeField] Color defaltColor = Color.white;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        coordsText = GetComponent<TextMeshPro>();
        Coordinates();
    }

    void Update()
    {
        LabelColor();
    }

    void Coordinates()
    {
        int unitySnapSize = gridManager.UnitySnapValue;
        coordinates.x = Mathf.RoundToInt(transform.position.x/unitySnapSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z/unitySnapSize);
        coordsText.text = coordinates.x + "," + coordinates.y;
        transform.parent.name = coordinates.x.ToString() + "," + coordinates.y.ToString();
    }

    void LabelColor()
    {
        
        Node node = gridManager.GetNode(coordinates);

        if(node != null)
        {
            if(node.isPath)
            {
                coordsText.color = pathColor;
            }
            else if(node.isExploride)
            {
                coordsText.color = explorideColor;
            }
            else if(node.isBlocked)
            {
                coordsText.color = blockedColor;
            }
            else
            {
                coordsText.color = defaltColor;
            }
        }
        else
        {
            coordsText.color = blockedColor;
        }
      
    }
}
