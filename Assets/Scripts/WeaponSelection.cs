using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    ScoreBoard scoreBoard;
    public Temprory temprory;

    [SerializeField] WeaponType machineGun;
    [SerializeField] WeaponType laserGun;
    [SerializeField] WeaponType flameThrower;

    void Awake()
    {   
        int requiredCrrency = Mathf.RoundToInt(Mathf.Infinity);
        GameObject weapon = null;
        int damage = 0;
        temprory.TemproryObject(weapon,requiredCrrency,damage);
    }

    public void MachineGun()
    {
        temprory.TemproryObject(machineGun.Weapon,machineGun.requiredCurrencyToPlace,machineGun.weaponDamage);       
    }

    public void FlameThrower()
    {
       temprory.TemproryObject(flameThrower.Weapon,flameThrower.requiredCurrencyToPlace,flameThrower.weaponDamage); 
    }

    public void LaserGun()
    {
       temprory.TemproryObject(laserGun.Weapon,laserGun.requiredCurrencyToPlace,laserGun.weaponDamage); 
    }
}
