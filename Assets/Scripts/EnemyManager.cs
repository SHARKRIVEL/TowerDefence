using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    //All enemies data
    [System.Serializable]
    public class poolData
    {
        public int rank;
        public GameObject prefab;
        public int Size = 10;
        public List<GameObject> pool;
    }
    public List<poolData> enemyPoolsData = new List<poolData>();
    Dictionary<int,List<GameObject>> enemyPools = new Dictionary<int, List<GameObject>>();

    //Level ScriptableObjects
    public List<LevelData> levelSbs;
    [SerializeField] TMP_Text waveText;
    [SerializeField] int Waves = 10;
    int Wave = 0;
    [SerializeField] int enemyCount = 10;
    public List<GameObject> enemies;

    ScoreBoard scoreBoard;
    GameManager gameManager;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
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
                GameObject gb = Instantiate(pl.prefab,transform.position,Quaternion.identity,transform);
                pl.pool.Add(gb);
                gb.SetActive(false);
            }
            enemyPools[pl.rank] = pl.pool;
        }
    }

    void Invoker()
    {
        if(Wave<Waves)
        {
            waveText.text = "WAVE : "+Wave +"/"+Waves;
            Invoke("PoolStarter",3f);
        }
        else
        {
            DataStorerBTWScenes.instance.LevelManager();
            gameManager.OnPlayAgain();
        }
    }

    void PoolStarter()
    {
        StartCoroutine(EnemyStarter());
    }

    IEnumerator EnemyStarter()
    {
        Debug.Log(Wave);
        enemies = enemyPools[levelSbs[DataStorerBTWScenes.instance.Level].waveData[Wave].rank];
        for(int i = 0;i<enemies.Count;i++)
        {
            yield return new WaitForSeconds(1f);
            enemies[i].SetActive(true);
        }
        Wave++;
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
