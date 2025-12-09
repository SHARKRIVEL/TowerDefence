using UnityEngine;
using TMPro;

public class GunTools : MonoBehaviour
{
    ScoreBoard scoreBoard;
    Tower tower;
    [SerializeField]Weapon weapon;

    [SerializeField]WeaponType weaponType;
    GridManager gridManager;

    int currencyForUpgrade;
    int fireRate;


    [SerializeField] TMP_Text weaponLevelText;
    [SerializeField] GameObject gunTools;
    [SerializeField] GameObject Parent;
    int level = 1;
    int maxLevel = 4;
    int valueToGetHalf = 2;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
        tower = GetComponentInParent<Tower>();
        currencyForUpgrade = weaponType.RequiredCurrencyForWeaponUpgrade;
        fireRate = weaponType.FireRate;
    }

    public void GunToolsActivation()
    {
        gunTools.SetActive(true);
        Invoke("GunToolsDeacctivation",3f); 
    }

    void GunToolsDeacctivation()
    {
        gunTools.SetActive(false);
    }

    public void OnUpgradeWeapon()
    {
        if(scoreBoard.currency>=currencyForUpgrade)
        {
            if(level<maxLevel)
            {
                level++;
                weaponLevelText.text = "LEVEL : "+level;
                WeaponUpgrading();
            }  
            else if(level == maxLevel)
            {
                level++;
                weaponLevelText.text = "MAXED";
                WeaponUpgrading();
            }  
        }
    }

    public void OnDestroyWeapon()
    {
        scoreBoard.CurrencyManager(currencyForUpgrade/valueToGetHalf);
        Vector2Int newCoordinates = gridManager.PositionToCoordinates(transform.parent.position);
        gridManager.Grid[newCoordinates].isWakable = true;
        Destroy(Parent);
    }

    void WeaponUpgrading()
    {
        weapon.BulletDamager(fireRate);
        scoreBoard.CurrencyManager(-currencyForUpgrade);
        currencyForUpgrade += currencyForUpgrade;
        tower.Building();
    }
}
