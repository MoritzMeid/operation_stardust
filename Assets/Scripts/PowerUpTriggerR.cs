using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTriggerR : MonoBehaviour
{
    private GameController gameController;

   
   

    public float PowerUpTime;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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

        yield break;

    }
}
