using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

   public void ToggleEndMenu(float score)
    {
        gameObject.SetActive(true);
        ScoreText.text = "Score: " + (score + 10);
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
