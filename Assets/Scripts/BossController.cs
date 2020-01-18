using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(true);
    }

    public void ToggleBoss()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().NextLevel();
    }

}
