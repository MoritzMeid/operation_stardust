using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevelMenu : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ToggleNextMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
