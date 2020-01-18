using System.Collections;
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
   
  

    public int damage;
    public GameOverMenu gameOverMenu;
    public NextLevelMenu nextLevelMenu;
    public BossController bossController;
    public TextMeshProUGUI tmpScore;
    public TextMeshProUGUI healthText;

    Coroutine wavesSpawner;
    

    private void Start()
    {
        gameOver = false;
        restart = false;
       
        playerHealth = 3;
        UpdateHealth();
  
        score = 0;
        UpdateScore();
        wavesSpawner = StartCoroutine (SpawnWaves());
        PlayerPrefs.GetInt("currentScore");
    }


  

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                if (score >= 200 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
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
        tmpScore.text = "Score: " + score;
    }

     void currentScore ()
    {
        PlayerPrefs.SetInt("currentScore", score);
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
        if (score >= 200 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1")) //if you reach the highscore of 200
        {
            NextLevel();
            //SceneManager.LoadScene("Level2");
        }
        if (score >= 250 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2")) //if you reach the highscore of 200
        {
            
            StartCoroutine(StartFinalBattle()); //Endboss
            StopCoroutine(wavesSpawner);
        }
        if (score >= 300 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2")) //if you reach the highscore of 200
        {
            NextLevel();
        }
        if (score >= 400 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3")) //if you reach the highscore of 300
        {
            NextLevel();
        }
    }

    IEnumerator StartFinalBattle()
    {
        
        yield return new WaitForSeconds(4);
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
    }

    public void GameOver()
    {
        gameOverMenu.ToggleEndMenu(score);
        Time.timeScale = 0f;

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
