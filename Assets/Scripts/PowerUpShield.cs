using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    public float PowerUpTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Shield());


        }
    }

    IEnumerator Shield ()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("PowerupH").gameObject.SetActive(false);

        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(PowerUpTime);

        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);

        Destroy(gameObject);

        yield break;

    }
}
