using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("dialoguelevel1");
        Time.timeScale = 1f;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void LevelDialogueOne()
    {
        SceneManager.LoadScene("dialoguelevel1");
        Time.timeScale = 1f;
    }
    public void LevelDialogueTwo()
    {
        SceneManager.LoadScene("dialoguelevel2");
        Time.timeScale = 1f;
    }
    public void LevelDialogueThree()
    {
        SceneManager.LoadScene("dialoguelevel3");
        Time.timeScale = 1f;
    }
    public void LevelDialogueFour()
    {
        SceneManager.LoadScene("dialoguelevel4");
        Time.timeScale = 1f;
    }
    public void LevelOne()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene("Level2");
        Time.timeScale = 1f;
    }
    public void LevelThree()
    {
        SceneManager.LoadScene("Level3");
        Time.timeScale = 1f;
    }
    public void LevelFour()
    {
        SceneManager.LoadScene("Level4");
        Time.timeScale = 1f;
    }
    public void LevelFive()
    {
        SceneManager.LoadScene("Level5");
        Time.timeScale = 1f;
    }

    public void LevelTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
