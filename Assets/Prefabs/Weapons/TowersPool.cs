using UnityEngine;
using System;
using System.Collections.Generic;

public class TowersPool : MonoBehaviour
{
    public List<enemy> enemies = new List<enemy>();

    void OnEnable()
    {
        enemy.OnSpawned += ManageEnemies;
    }

    void OnDisable()
    {
        enemy.OnSpawned -= ManageEnemies;
    }

    void ManageEnemies(enemy ey,EnemyStates enemyState)
    {
        if(enemyState == EnemyStates.Active) enemies.Add(ey);
        else enemies.Remove(ey);
    }
}
