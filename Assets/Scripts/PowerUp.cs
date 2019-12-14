using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private GameController gameController;
    public int scoreValue;

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (other.tag == "Player")
        {
            gameController.PowerUpH();


        }
        gameController.AddScore(scoreValue);
        Destroy(gameObject);

    }
}