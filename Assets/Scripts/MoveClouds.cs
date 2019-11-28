using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClouds : MonoBehaviour
{
    public float speed;
    public Rigidbody rbc;
    void Start()
    {
        rbc = GetComponent<Rigidbody>();
        rbc.velocity = transform.up * speed;

    }


}
