using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private GameController gameController;
    public GameObject pickupEffect;
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



    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.PowerUpH();
            Pickup(other);
            Destroy(gameObject);

        }
    }

    void Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //effect

        Destroy(gameObject);
    }
}