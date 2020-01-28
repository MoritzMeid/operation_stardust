using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public AudioSource GameOverAudio;
    private float oldVolume;

    void Start()
    {
        gameObject.SetActive(false);
    }

   public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        oldVolume = AudioListener.volume;
       
        //AudioListener.volume = 0.2f;
        GameOverAudio.volume = 3.0f;
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
