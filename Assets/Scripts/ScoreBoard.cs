using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    DataStorerBTWScenes dataStorerBTWScenes;

    int currencyRef = 0;
    public bool gamePos = false;
    public int currency = 0;
    public int towerHealth = 10;

    [SerializeField] TMP_Text towerHealthText;
    [SerializeField] TMP_Text currencyText;
    [SerializeField] GameObject gameReplayButtons;

    void Start()
    {
        dataStorerBTWScenes = FindFirstObjectByType<DataStorerBTWScenes>();
        TowerHealth(0);
        CurrencyManager(dataStorerBTWScenes.currency);
    }

    public void TowerHealth(int healthDec)
    {
        towerHealth -= healthDec;
        towerHealthText.text = "TowerHealth : " + towerHealth;
        if(towerHealth <= 0)
        {
            ActivateGameManager();
        }
    }

    public void ActivateGameManager()
    {
        gamePos = true;
        gameReplayButtons.SetActive(true);
    }

    public void CurrencyManager(int currencyInc)
    { 
        currency += currencyInc;
        currencyText.text = "Currency : " + currency;
    }
}
