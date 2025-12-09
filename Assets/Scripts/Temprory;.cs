using UnityEngine;

[CreateAssetMenu(fileName = "Temprory", menuName = "Scriptable Objects/Temprory")]
public class Temprory : ScriptableObject
{
    public GameObject weapon;
    public int requiredCrrency;
    public int damage;

    public void TemproryObject(GameObject weapon,int requiredCrrency,int damage)
    {
        this.weapon = weapon;
        this.requiredCrrency = requiredCrrency;
        this.damage = damage;
    }
}
