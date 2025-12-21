using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData",menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int currencyForEnemyDestroy;
    public float speed;
    public int health;
    public int healthInc;
    public int scoreForDeath;
    public int damageToTower;
}
