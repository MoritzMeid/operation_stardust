using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GetScore : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI currentScoreText;
    // Start is called before the first frame update
    void Start()
    { 
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
        currentScoreText.text = "Score: " + PlayerPrefs.GetInt("currentScore");
    }
}
