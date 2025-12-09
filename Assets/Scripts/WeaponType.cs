using UnityEngine;

[CreateAssetMenu(fileName = "WeaponType", menuName = "Scriptable Objects/WeaponType")]
public class WeaponType : ScriptableObject
{
    public int index;
    public int requiredCurrencyToPlace = 400;
    public GameObject Weapon;
    public int WeaponDamage = 4;

    public int FireRate;

    public int FireRateIncrement;

    public int RequiredCurrencyForWeaponUpgrade;

    
}
