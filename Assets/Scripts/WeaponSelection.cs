using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    ScoreBoard scoreBoard;
    public Temprory temprory;

    [SerializeField] MachineGun machineGun;
    [SerializeField] LaserGun laserGun;
    [SerializeField] FlameThrower flameThrower;

    void Awake()
    {   
        int requiredCrrency = Mathf.RoundToInt(Mathf.Infinity);
        GameObject weapon = null;
        int damage = 0;
        temprory.TemproryObject(weapon,requiredCrrency,damage);
    }

    public void MachineGun()
    {
        temprory.TemproryObject(machineGun.machineGun,machineGun.requiredCurrencyForMachineGun,machineGun.machineGunDamage);       
    }

    public void FlameThrower()
    {
        Debug.Log(flameThrower.requiredCurrencyForFlameThrower);
       temprory.TemproryObject(flameThrower.flameThrower,flameThrower.requiredCurrencyForFlameThrower,flameThrower.flameThrowerDamage); 
    }

    public void LaserGun()
    {
       temprory.TemproryObject(laserGun.laserGun,laserGun.requiredCurrencyForLaserGun,laserGun.laserGunDamage); 
    }
}
