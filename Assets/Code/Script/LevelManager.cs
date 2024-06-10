using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerHp playerHp;
    public static LevelManager main;

    public Transform StartPoint;
    public Transform[] path;
    EnemySpawner enemycount;

    public GameOverUI gameOverUI;
    public GameObject levelCompleteCanvas;
    public int currency;

    public int kill = 0;
    public int requiredKills = 8;
    public string nextSceneName;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 1000000;
        levelCompleteCanvas.SetActive(false);
    }

    private void Update()
    {
        if (kill >= requiredKills)
        {
            ShowLevelComplete();
        }
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if(amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not Enough Money"); 
            return false;
        }
    }

    public void GameOver()
    {
        if (PlayerHpPoint.startHP <= 0)
        {
            Debug.Log("Game Over!"); 
            gameOverUI.ShowGameOver(); 
            Time.timeScale = 0; 
        }
    } 

    public void killpoint()
    {
        kill++;
        Debug.Log("Kill count: " + kill);
    }
    private void ShowLevelComplete()
    {
        Debug.Log("Level Complete!");
        levelCompleteCanvas.SetActive(true); 
        Time.timeScale = 0; 
    }

    public void OnNextLevelButtonPressed()
    {
        SceneManager.LoadScene("Level2"); 
        Time.timeScale = 1; 
    }
}
