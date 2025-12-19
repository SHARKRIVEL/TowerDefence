using UnityEngine;

[CreateAssetMenu(fileName = "WeaponType", menuName = "Scriptable Objects/WeaponType")]
public class WeaponType : ScriptableObject
{
    public int index;
    public int requiredCurrencyToPlace;
    public GameObject Weapon;
    public int weaponDamage;
    public float FireRate;
    public float FireRateIncrement;
    public int RequiredCurrencyForWeaponUpgrade;
}
