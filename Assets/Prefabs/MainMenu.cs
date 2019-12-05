using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Level1");
    }

    public void ClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ClickCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ClickLevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void RestartGame()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelOne()
    {
        SceneManager.LoadScene("Level1");
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }
    public void LevelThree()
    {
        SceneManager.LoadScene("Level3");
    }
    public void LevelFour()
    {
        SceneManager.LoadScene("Level4");
    }
    public void LevelFive()
    {
        SceneManager.LoadScene("Level5");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
