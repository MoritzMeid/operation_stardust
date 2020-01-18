using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpS : MonoBehaviour
{
    public float PowerUpTime;
    public float multiplier = 2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Size());


        }
    }

    IEnumerator Size()
    {

        GameObject.FindWithTag("Player").transform.localScale /= multiplier;

        GameObject.FindGameObjectWithTag("PowerUpS").transform.GetChild(0).gameObject.SetActive(false);

 

        yield return new WaitForSeconds(PowerUpTime);

        GameObject.FindWithTag("Player").transform.localScale *= multiplier;

        Destroy(gameObject);

        yield break;
    }
}