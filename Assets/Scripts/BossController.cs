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

    public void ToggleBoss()
    {
        gameObject.SetActive(true);
    }

    public void Update()
    {
        if(bossHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void OnTriggerEnter(Collider other)
    {

        SubBossHealth();

    }

    public void SubBossHealth()
    {
        bossHealth -= 1;
    }

    private void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().NextLevel();
    }

}
