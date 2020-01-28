using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    AudioSource audioSource;
    public float PowerUpTime;
    public AudioClip shield;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //audioSource = GetComponent<AudioSource>();
            StartCoroutine(Shield());
            

        }
    }

    IEnumerator Shield ()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("PowerupH").gameObject.SetActive(false);

        //audioSource.Play();

        AudioSource.PlayClipAtPoint(shield, new Vector3(0, 12, 7));

        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(PowerUpTime);

        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);

        Destroy(gameObject);

        yield break;

    }
}
