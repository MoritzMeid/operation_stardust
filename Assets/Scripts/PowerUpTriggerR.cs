using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTriggerR : MonoBehaviour
{
    private GameController gameController;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public float PowerUpTime;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().powerUp = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().slider.value = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().weaponHeat = 0;
            audioSource.Play();
            StartCoroutine(MultiShot());

            

        }
    }

    IEnumerator MultiShot ()

    {


        GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpR>().enabled = true;

        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;

        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(PowerUpTime);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpR>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().powerUp = false;
        Destroy(gameObject);
        yield break;
        
    }
}
