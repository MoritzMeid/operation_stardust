using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public GameObject[] hazards2;
    public GameObject[] hazards3;

    public Vector3 spawnValues;
    public Vector3 spawnValuesEnemy;
    public int hazardCount;
    private int hazardCountOrigin; 
    public float spawnWait;
    public float startWait;
    public float waveWait;
    
    public static float playerHealth;
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

    private bool bossActive;

    private bool crs_runnig;
    private int shieldDuration; 




    public AudioSource hitSound;

    Coroutine wavesSpawner;
    Coroutine ShieldDuration;
    

    private void Start()
    {

        bossActive = false;
        warning = GameObject.FindGameObjectWithTag("Warning");
        warning.SetActive(false);

        gameOver = false;
        restart = false;

        hazardCountOrigin = hazardCount;

        score = 0;

        playerHealth = 3;
        UpdateHealth();
  
        UpdateScore();
        
        if (!(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level5")))
        {
            wavesSpawner = StartCoroutine(SpawnWaves());

        }
        else
        {
            wavesSpawner = StartCoroutine(SpwanEndlessWaves());
        }

        //if (PlayerPrefs.HasKey("Score"))
        //{
        //    if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1"))
        //    {
        //        PlayerPrefs.DeleteKey("Score");
        //        score = 0;
        //    }
        //    else
        //    {
        //        score = PlayerPrefs.GetInt("Score");
        //    }
        //}
    }
  
    IEnumerator SpawnWaves()
    {
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
               
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);

                    yield return new WaitForSeconds(spawnWait);

                
                
            }
            yield return new WaitForSeconds(waveWait);
            hazardCount++;
        }
    }

    IEnumerator SpwanEndlessWaves()
    {


        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                if(score <= 100)
                {
                
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);

                }
                   else if (score <= 4000)
                {

                    GameObject hazard = hazards2[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);

                    yield return new WaitForSeconds(spawnWait);

                }
                    else
                {

                    GameObject hazard = hazards3[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);

                    yield return new WaitForSeconds(spawnWait);

                }


            }
            yield return new WaitForSeconds(waveWait);
            if(hazardCount > 15)
            {
                hazardCount = hazardCountOrigin;
            }else
            {
                hazardCount++;
            }
           
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

    //void SaveScore()
    //{
    //    PlayerPrefs.SetInt("Score", score);
    //}

    void NextLevelScore()
    {
        if (score >= 300 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1")) //if you reach the highscore of 200
        {
            //SaveScore();
            NextLevel();
        }
        //LEVEL 2
        if (score >= 500 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2") && bossActive == false) //Initiate Boss Battle
        {
            
            StartCoroutine(StartFinalBattle()); //Endboss
            StopCoroutine(wavesSpawner);
            bossActive = true;
        }
            if (score >= 600 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2")) //Next level
            {
                NextLevel();
            }

        //LEVEL 3
        if (score >= 600 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3") && bossActive == false) 
        {
            StartCoroutine(StartFinalBattle()); //Endboss
            StopCoroutine(wavesSpawner);
            bossActive = true;
        }
            if (score >= 700 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3")) //Next level
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
        Destroy(warning);
        BossBattle();
    }


    public void BossBattle()
    {
        bossController.ToggleBoss();
    }

    public void NextLevel()
    {
        PlayerPrefs.Save();
        nextLevelMenu.ToggleNextMenu();
        Time.timeScale = 0f;
        warning.SetActive(false);
    }

    public void GameOver()
    {
        gameOverMenu.ToggleEndMenu();
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
            hitSound.Play();
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

    public void ToggleShield()
    {

        if (crs_runnig)
        {
            shieldDuration += 5;
        }
        else
        {
            shieldDuration = 5;
            ShieldDuration = StartCoroutine(ShieldActive());
        }
        //ToDo abgleich ob Schild aktiv
        //wenn ja, dann int addieren 
        // wenn nein, dann starten 
    }

     IEnumerator ShieldActive()
    {
        crs_runnig = true;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(true);
        while (shieldDuration >= 0)
        {
            yield return new WaitForSeconds(1);
            shieldDuration--;
        }

        crs_runnig = false;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);
        yield break; 
    }
    

}



