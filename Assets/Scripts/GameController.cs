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
    public TextMeshProUGUI tmpScore;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        gameOver = false;
        restart = false;
        playerHealth = 3;
        UpdateHealth();
        // restartText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
        PlayerPrefs.GetInt("currentScore");
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        Application.LoadLevel(Application.loadedLevel);
    //        hazardCount = 1;
    //    }
    //}

    //IEnumerator SpawnWaves()
    //{
    //    yield return new WaitForSeconds(startWait);

    //    while (true)
    //    {
    //        for (int i = 0; i < hazardCount; i++)
    //        {
    //            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
    //            Vector3 spawnPosition2 = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
    //            Quaternion spawnRotation = Quaternion.identity;

    //            Instantiate(enemy, spawnPosition2, spawnRotation);
    //            Instantiate(enemy_02, spawnPosition2, spawnRotation);
    //            Instantiate(hazard, spawnPosition, spawnRotation);
    //            Instantiate(hazard_02, spawnPosition, spawnRotation);
    //            Instantiate(hazard_03, spawnPosition, spawnRotation);

    //            yield return new WaitForSeconds(spawnWait);
    //        }
    //        yield return new WaitForSeconds(waveWait);
    //        hazardCount++;
    //    }
    //}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

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
        if (score >= 150 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1")) //if you reach the highscore of 150
        {
            SceneManager.LoadScene("Level2");
        }
    }

    public void GameOver()
    {
        //gameOverText.text = "Game Over";
        //gameOver = true;
        SceneManager.LoadScene("GameOver");
    }

    public void SubHealth()
    {

        playerHealth -= 1;
        UpdateHealth();

        //if (playerHealth == 0)
        //{

        //    GameOver();
        //}

    }
    public void UpdateHealth()
    {
        healthText.text = "Health: " + playerHealth;

    }

}
