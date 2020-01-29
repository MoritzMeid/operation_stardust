using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpH : MonoBehaviour
{
    private GameController gameController;
    public GameObject pickupEffect;
    public int scoreValue;
    public AudioClip heart; 

    private void Start()
    {
        
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannoit find GameController");
        }

    }



    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.PowerUpH();
            AudioSource.PlayClipAtPoint(heart, new Vector3(0, 12, 7));
            Destroy(gameObject);

        }
    }

    
}