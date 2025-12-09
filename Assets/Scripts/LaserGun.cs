using UnityEngine;

[CreateAssetMenu(fileName = "LaserGun", menuName = "Scriptable Objects/LaserGun")]
public class LaserGun : ScriptableObject
{
    public int requiredCurrencyForLaserGun = 200;
    public GameObject laserGun;
    public int laserGunDamage = 3;
}
