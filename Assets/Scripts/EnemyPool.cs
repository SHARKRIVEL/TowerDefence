using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class poolData
    {
        public int rank;
        public GameObject prefab;
        public int Size = 10;
        public List<GameObject> pool;
    }

    public List<poolData> enemyPoolsData = new List<poolData>();
    public Dictionary<int,List<GameObject>> enemyPools = new Dictionary<int,List<GameObject>>();

    public List<LevelData> levelData;

    ScoreBoard scoreBoard;
    GameManager gameManager;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] TMP_Text waveText;
    [SerializeField] int enemySize = 10;
    [SerializeField] int Waves = 10;
    int Wave = 1;
    public int enemyDamage = 2;
    bool poolCompleted = false;

    List<GameObject> enemies;
    int enemyCount = 15;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
        enemies = new List<GameObject>(enemySize);
        EnemyPooling();
    }

    void Start()
    {
        Invoker();
    }

    void EnemyPooling()
    {
        foreach(poolData pl in enemyPoolsData)
        {
            pl.pool = new List<GameObject>();
            for(int i=0;i<pl.Size;i++)
            {
                GameObject gb = Instantiate(pl.prefab,transform);
                pl.pool.Add(gb);
                gb.SetActive(false);
            }
            enemyPools[pl.rank] = pl.pool;
        }
    }

    void Invoker()
    {
        if(Wave<=Waves)
        {
            waveText.text = "WAVE : "+Wave +"/"+Waves;
            Invoke("PoolStarter",3f);
            Wave++;
        }
        else
        {
            DataStorerBTWScenes.instance.LevelManager(1);
            gameManager.OnPlayAgain();
        }
    }

    void PoolStarter()
    {
        StartCoroutine(EnemyStarter());
    }

    IEnumerator EnemyStarter()
    {
        enemies = enemyPools[levelData[DataStorerBTWScenes.instance.Level].waveData[Wave].rank];
        for(int i = 0;i<enemyCount;i++)
        {
            enemies[i].SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

    public void Dead()
    {
        enemyCount--;
        if(scoreBoard.towerHealth>0 && enemyCount == 0)
        {
            enemyCount = 10;
            Invoker();
        }
    }
}
