using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemy : MonoBehaviour
{
    [SerializeField] int enemyHealth;
    [SerializeField] int enemyHealthInc = 10;
    int enemyHealthRef;
    List<Node> Path = new List<Node>();

    [SerializeField] MachineGun machineGun;
    [SerializeField] FlameThrower flameThrower;
    [SerializeField] LaserGun laserGun;

    PathFinder pathFinder;
    GridManager gridManager;
    EnemyPool enemyPool;
    ScoreBoard scoreBoard;
    DataStorerBTWScenes dataStorerBTWScenes;

    bool firing;
    float lerpMaxVal = 1f;
    float lerpVal = 0f;
    int scoreForEnemyDeath = 10;
    int twoerHealthDec = 1;
    [SerializeField] int weaponBulletDamage = 2;
    [SerializeField] float enemySpeed = 5f;
    [SerializeField] int currencyForEnemyDestroy = 15;

    void Awake()
    {
        dataStorerBTWScenes = FindFirstObjectByType<DataStorerBTWScenes>();
        enemyPool = GetComponentInParent<EnemyPool>();
        gridManager = FindFirstObjectByType<GridManager>();
        pathFinder = FindFirstObjectByType<PathFinder>(); 
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
        enemyHealth = dataStorerBTWScenes.enemyHealth;
    }

    void OnEnable()
    { 
        firing = true;
        enemyHealthRef = enemyHealth;
        NewPath(true);
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
                    lerpVal += Time.deltaTime*enemySpeed;
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        if(scoreBoard.towerHealth>0)
        {
            scoreBoard.TowerHealth(twoerHealthDec);
        }
        EnemyDeadState();
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("MachineGun"))
        {
            weaponBulletDamage =  machineGun.machineGunDamage;
        }
        if(other.CompareTag("FlameGun"))
        {
            weaponBulletDamage = flameThrower.flameThrowerDamage;
        }
        if(other.CompareTag("LaserGun"))
        {
            weaponBulletDamage = laserGun.laserGunDamage;
        }
        enemyHealthRef -= weaponBulletDamage;
        if(enemyHealthRef<=0 && firing)
        {
            firing = false;
            dataStorerBTWScenes.ScoreManager(scoreForEnemyDeath);
            scoreBoard.CurrencyManager(currencyForEnemyDestroy);
            EnemyDeadState();
        }
    }

    void EnemyDeadState()
    {
        enemyHealth += enemyHealthInc;
        gameObject.SetActive(false);
        transform.position = gridManager.CoordinatesToPosition(pathFinder.StartCoords);
        enemyPool.EnemyCollector(gameObject);
    }
}
