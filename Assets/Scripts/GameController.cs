using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public GameObject hazard_02;
    public GameObject enemy;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public static float playerHealth;
   // public GUIText scoreText;
    public GUIText healthText;
    private int score;
    private bool gameOver;
    private bool restart;
    public int damage;
    public TextMeshProUGUI tmpScore;

    private void Start()
    {
        gameOver = false;
        restart = false;
       // restartText.text = "";
        // playerHealth = 3;
        // UpdateHealth();
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        Application.LoadLevel(Application.loadedLevel);
    //        hazardCount = 1;
    //    }
    //}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
       
        while (true)
            {
            for (int i = 0; i < hazardCount; i++)
            {
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    Instantiate(hazard_02, spawnPosition, spawnRotation);
                    Instantiate(enemy, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

                }
            yield return new WaitForSeconds(waveWait);
            hazardCount++;

            //if (gameOver)
            //{
            //    restartText.text = "Drücke 'R' um es erneut zu versuchen!";

            //    restart = true;
            //    break;

            //}
        }
        
    }   

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore ()
    {
        tmpScore.text = "Score: " + score;
    }

    public void GameOver()
    {
        //gameOverText.text = "Game Over";
        //gameOver = true;
        SceneManager.LoadScene("GameOver");
    }

 /*   public void SubHealth ()
    {
        
        playerHealth -= 1;
        UpdateHealth();
        if (playerHealth == 0)
        {   

            gameOver = true;
        }

    }
    public void UpdateHealth()
    {
        healthText.text = "HP: " + playerHealth;

    }

*/
 
}
