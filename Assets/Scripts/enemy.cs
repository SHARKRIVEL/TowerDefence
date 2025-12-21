using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class enemy : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    [SerializeField] GameObject blastParticle;
    List<Node> Path = new List<Node>();
    [SerializeField] WeaponType machineGun;
    [SerializeField] WeaponType flameThrower;
    [SerializeField] WeaponType laserGun;

    PathFinder pathFinder;
    GridManager gridManager;
    EnemyManager enemyManager;
    ScoreBoard scoreBoard;

    [SerializeField] int enemyHealth;
    int weaponBulletDamage;

    bool firing;
    float lerpMaxVal = 1f;
    float lerpVal = 0f;

    public static event Action<enemy,EnemyStates> OnSpawned;

    void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        gridManager = FindFirstObjectByType<GridManager>();
        pathFinder = FindFirstObjectByType<PathFinder>();
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
    }

    void OnEnable()
    {
        enemyHealth = enemyData.health;
        OnSpawned?.Invoke(this,EnemyStates.Active); 
        firing = true;
        NewPath(true);
    }

    void OnDisable()
    {
        OnSpawned?.Invoke(this,EnemyStates.InActive);
    }

    void NewPath(bool newPath)
    { 
        Vector2Int coordinates = new Vector2Int();
        if(newPath)
        {
            coordinates = pathFinder.StartCoords;
        }
        else
        {
            coordinates = gridManager.PositionToCoordinates(transform.position);
        }
        StopAllCoroutines();
        Path.Clear();
        Path = pathFinder.RecalculatePath(coordinates);
        StartCoroutine(EnemyPath());
    }

    IEnumerator EnemyPath()
    {
        for(int i = 1;i<Path.Count;i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.CoordinatesToPosition(Path[i].coordinates);

            transform.LookAt(endPos);
            lerpVal = 0f;
            if(!scoreBoard.gamePos)
            {
                while(lerpVal<lerpMaxVal)
                {
                    transform.position = Vector3.Lerp(startPos,endPos,lerpVal);
                    lerpVal += Time.deltaTime*enemyData.speed;
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        if(scoreBoard.towerHealth>0)
        {
            scoreBoard.TowerHealth(enemyData.damageToTower);
        }
        EnemyDeadState();
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("MachineGun"))
        {
            weaponBulletDamage =  machineGun.weaponDamage;
        }

        if(other.CompareTag("FlameGun"))
        {
            weaponBulletDamage = flameThrower.weaponDamage;
        }

        if(other.CompareTag("LaserGun"))
        {
            weaponBulletDamage = laserGun.weaponDamage;
        }
        

        enemyHealth -= weaponBulletDamage;
        if(enemyHealth<=0 && firing)
        {
            firing = false;
            DataStorerBTWScenes.instance.ScoreManager(enemyData.scoreForDeath);
            scoreBoard.CurrencyManager(enemyData.currencyForEnemyDestroy);
            EnemyDeadState();
        }
    }

    void EnemyDeadState()
    {
        Instantiate(blastParticle,transform.position + Vector3.up*5f,Quaternion.identity);
        gameObject.SetActive(false);
        transform.position = gridManager.CoordinatesToPosition(pathFinder.StartCoords);
        enemyManager.Dead();
    }
}
