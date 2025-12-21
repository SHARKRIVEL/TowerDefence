using UnityEngine;
using TMPro;

public class DataStorerBTWScenes : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text levelText;
    public int currency = 75;
    int currencyIncByLevel = 75;
    public int Level = 0;
    int Score = 0;
    public static DataStorerBTWScenes instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance  = this;
        DontDestroyOnLoad(gameObject);
        ScoreManager(Score);
        //LevelManager(1);
    }

    public void ScoreManager(int score)
    {
        Score += score;
        scoreText.text = "SCORE : "+ Score;
    }

    public void LevelManager()
    {
        //enemyHealth += enemyHealthInc;
        currency += currencyIncByLevel;
        Level++;
        levelText.text = "LEVEL : "+Level;
    }
}
