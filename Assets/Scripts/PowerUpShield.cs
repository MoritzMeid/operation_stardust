using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    AudioSource audioSource;
    public float PowerUpTime;
    public AudioClip shield;
    private GameController gameController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("PowerupH").gameObject.SetActive(false);

            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            gameController = gameControllerObject.GetComponent<GameController>();
            gameController.ToggleShield();
            AudioSource.PlayClipAtPoint(shield, new Vector3(0, 12, 7));
            Destroy(gameObject);

            //audioSource = GetComponent<AudioSource>();
            //StartCoroutine(Shield());


        }
    }

    //IEnumerator Shield ()
    //{
        

    //    GameObject.FindGameObjectWithTag("PowerupH").gameObject.SetActive(false);

    //    AudioSource.PlayClipAtPoint(shield, new Vector3(0, 12, 7));

    //    GetComponent<Collider>().enabled = false;

    //    yield return new WaitForSeconds(PowerUpTime);

    //    GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);

    //    Destroy(gameObject);

    //    yield break;

    //}
}
