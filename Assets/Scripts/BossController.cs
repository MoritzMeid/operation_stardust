using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int bossHealth;

    void Start()
    {
        gameObject.SetActive(true);

    }

    private void Update()
    {
        if (bossHealth==0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")||(other.CompareTag("Bolt")))
        {
            bossHealth -= 1;

        }
    }

    public void ToggleBoss()
    {
        gameObject.SetActive(true);

    }
}
