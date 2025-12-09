using UnityEngine;

[CreateAssetMenu(fileName = "FlameThrower", menuName = "Scriptable Objects/FlameThrower")]
public class FlameThrower : ScriptableObject
{
    public int requiredCurrencyForFlameThrower = 400;
    public GameObject flameThrower;
    public int flameThrowerDamage = 4;
}
