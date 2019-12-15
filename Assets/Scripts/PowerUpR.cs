using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpR : MonoBehaviour
{
    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRate;
    private Rigidbody rb;
    private AudioSource audioSource;
    private float nextFire;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {


        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            foreach (var shotSpawn in shotSpawns)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                audioSource.Play();
            }

        }
    }
}

