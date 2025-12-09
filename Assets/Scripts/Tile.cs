using UnityEngine;

public class Tile : MonoBehaviour
{
    Vector2Int coordinates;
    [SerializeField] bool isPlacable = true;
    GridManager gridManager;
    PathFinder pathFinder;
    ScoreBoard scoreBoard;
    WeaponSelection weaponSelection;
    [SerializeField] Temprory temprory;

    [SerializeField] int requiredCurrencyForTowerPlacement;

    void Awake()
    {
        weaponSelection = FindFirstObjectByType<WeaponSelection>();
        gridManager = FindFirstObjectByType<GridManager>();
        pathFinder = FindFirstObjectByType<PathFinder>();
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.PositionToCoordinates(transform.position);
            if(!isPlacable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseOver()
    {
        if(temprory.weapon == null){ return; }
        if(gridManager.Grid.ContainsKey(coordinates))
        {
            WeaponPlacing();
        }
    }

    void WeaponPlacing()
    {
        if(gridManager.Grid[coordinates].isWakable && Input.GetMouseButtonDown(1) && scoreBoard.currency >= temprory.requiredCrrency)
        {
            if(!pathFinder.WillBlockPath(coordinates))
            {
                bool isSuccessful = Instantiate(temprory.weapon,transform.position,Quaternion.identity);
                scoreBoard.CurrencyManager(-temprory.requiredCrrency);
                if(isSuccessful)
                {
                    gridManager.BlockNode(coordinates);
                    isPlacable = false;
                    pathFinder.NotifyReceivers();
                }
            }
        }
    }
}
