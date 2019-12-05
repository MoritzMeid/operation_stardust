using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
    private float deltaX, deltaY, deltaZ;

    private Rigidbody rb;
    public float speed;
    public float tilt; 
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawnLeftClick;
    public Transform shotSpawnRightClick;
    public float fireRate;

    private AudioSource audioSource;
    private float nextFire;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {


        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawnLeftClick.position, shotSpawnLeftClick.rotation);
            audioSource.Play();
        } else if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawnRightClick.position, shotSpawnLeftClick.rotation);
            audioSource.Play();
        }
        

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

    if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    deltaZ = touchPos.z - transform.position.z;
                    break;

                case TouchPhase.Moved:
                    rb.MovePosition(new Vector3(touchPos.x - deltaX, touchPos.y - deltaY, touchPos.z - deltaZ));
                    break;

                case TouchPhase.Ended:
                    rb.velocity = Vector3.zero;
                    break;
            }
        }
    }
}
