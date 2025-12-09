using UnityEngine;

[CreateAssetMenu(fileName = "MachineGun", menuName = "Scriptable Objects/MachineGun")]
public class MachineGun : ScriptableObject
{
    public int requiredCurrencyForMachineGun = 75;
    public GameObject machineGun;
    public int machineGunDamage = 2;
}
