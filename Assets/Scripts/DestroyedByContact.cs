using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log("Cannoit find GameController");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy") || other.CompareTag ("PowerUpR") || other.CompareTag("PowerupH")|| other.CompareTag("PowerUpS"))
        {
            return;
        }


        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        

        if (other.tag == "Player")
        {   

            gameController.SubHealth();
        }

        if (GameController.playerHealth == 0)
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

            gameController.GameOver();
        }

        if (!other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
        gameController.AddScore(scoreValue);
        //Destroy(other.gameObject); //Destroy Player
        Destroy(gameObject);
    }
}
