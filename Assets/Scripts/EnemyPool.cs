using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class EnemyPool : MonoBehaviour
{
    ScoreBoard scoreBoard;
    DataStorerBTWScenes dataStorerBTWScenes;
    EnemyPool enemyPool;
    GameManager gameManager;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] TMP_Text waveText;
    [SerializeField] int enemySize = 10;
    [SerializeField] int Waves = 10;
    int Wave = 1;
    public int enemyDamage = 2;
    bool poolCompleted = false;

    List<GameObject> EnemyStorer = new List<GameObject>();
    List<GameObject> enemies;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        dataStorerBTWScenes = FindFirstObjectByType<DataStorerBTWScenes>();
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
        enemyPool = FindFirstObjectByType<EnemyPool>();
        enemies = new List<GameObject>(enemySize);
        EnemyPooling();
    }

    void Start()
    {
        Invoker();
    }

    void EnemyPooling()
    {
        for(int i = 0;i<enemySize;i++)
        {
            GameObject enemy = Instantiate(enemyPrefab,transform);
            enemies.Add(enemy);
            enemy.SetActive(false);
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
            dataStorerBTWScenes.LevelManager(1);
            gameManager.OnPlayAgain();
        }
    }

    void PoolStarter()
    {
        StartCoroutine(EnemyStarter());
    }

    IEnumerator EnemyStarter()
    {
        enemySize = enemies.Count;;
        for(int i = 0;i<enemySize;i++)
        {
            enemies[i].SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

    public void EnemyCollector(GameObject gb)
    {
        EnemyStorer.Add(gb);
        if(EnemyStorer.Count == enemySize)
        {
            enemies.Clear();
            foreach(GameObject enemy in EnemyStorer)
            {
                enemies.Add(enemy);
            }
            EnemyStorer.Clear();
            if(scoreBoard.towerHealth>0)
            {
                Invoker();
            }
        }
    }
}
