﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    

    public Vector3 spawnValues;
    public Vector3 spawnValuesEnemy;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    
    public static float playerHealth;
   // public GUIText scoreText;
    private int score;
    private bool gameOver;
    private bool restart;

    public bool spawnAsteroid;
    public bool spawnEnemy;
   
    private GameObject warning;

    public int damage;
    public GameOverMenu gameOverMenu;
    public NextLevelMenu nextLevelMenu;
    public BossController bossController;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI healthText;

    Coroutine wavesSpawner;
    

    private void Start()
    {
        warning = GameObject.FindGameObjectWithTag("Warning");
        warning.SetActive(false);


        gameOver = false;
        restart = false;
       
        playerHealth = 3;
        UpdateHealth();
  
        score = 0;
        UpdateScore();
        wavesSpawner = StartCoroutine (SpawnWaves());
        PlayerPrefs.GetInt("currentScore");
        PlayerPrefs.GetInt("NextScore");
        PlayerPrefs.SetInt("currentScore", score);
    }


  

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                if (score >= 500 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
                {
                    yield break;
                }
                else
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);

                    yield return new WaitForSeconds(spawnWait);

                }
                
            }
            yield return new WaitForSeconds(waveWait);
            hazardCount++;
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
        NextLevelScore();
        setHighScore();
    }

    void UpdateScore ()
    {
        ScoreText.text = "Score: " + score;
        gameOverScore.text = "Score: " + score;
    }

    void setHighScore()
    {
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    void NextLevelScore()
    {
        if (score >= 300 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1")) //if you reach the highscore of 200
        {
            PlayerPrefs.SetInt("Nextscore", score);
            NextLevel();
            //SceneManager.LoadScene("Level2");
        }
        //LEVEL 2
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
        {
           PlayerPrefs.GetInt("Nextscore", score);
        }
        if (score >= 500 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2")) //Initiate Boss Battle
        {
            
            StartCoroutine(StartFinalBattle()); //Endboss
            StopCoroutine(wavesSpawner);
        }
            if (score >= 700 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2")) //Next level
            {
                NextLevel();
            }

        //LEVEL 3
        if (score >= 800 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3")) 
        {
            StartCoroutine(StartFinalBattle()); //Endboss
            StopCoroutine(wavesSpawner);
        }
            if (score >= 1000 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3")) //Next level
            {
                NextLevel();
            }
    }

    IEnumerator StartFinalBattle()
    {
        for(int i = 0; i<3; i++)
        {
            
            warning.SetActive(true);
            yield return new WaitForSecondsRealtime(1.6f);

            warning.SetActive(false);
            yield return new WaitForSecondsRealtime(1.6f);
        }
        BossBattle();
    }


    public void BossBattle()
    {
        bossController.ToggleBoss();
    }

    public void NextLevel()
    {
        nextLevelMenu.ToggleNextMenu();
        Time.timeScale = 0f;
        warning.SetActive(false);
    }

    public void GameOver()
    {
        gameOverMenu.ToggleEndMenu();
        Time.timeScale = 0f;
        warning.SetActive(false);

    }

    public void SubHealth()
    {
        if(GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.active==true)
        // if (GameObject.FindGameObjectWithTag("Player").transform.GetChild(0) == isActiveAndEnabled)
        {
            return;
        }
        else
        {

            playerHealth -= 1;
            UpdateHealth();

        }
  
    }

    public void PowerUpH()
    {
        playerHealth += 1;
        UpdateHealth();
    }


    public void UpdateHealth()
    {
        healthText.text = "Health: " + playerHealth;

    }

}
