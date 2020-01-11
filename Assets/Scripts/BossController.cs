using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ToggleBoss()
    {
        gameObject.SetActive(true);
    }
}
