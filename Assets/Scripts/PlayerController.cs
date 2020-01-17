using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Transform[] shotSpawns;
    public float fireRate;
    public int weaponHeat;
    public int weaponMAXHeat;
    private bool isHot;

    public AudioSource audioSource;

    private float nextFire;
    float initAccelY;
    float initAccelX;
    public Slider slider; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        slider = GameObject.Find("Magazin").GetComponent<Slider>();
        slider.value = 0;
        getInitAccl();

    }


    void getInitAccl()
    {
        initAccelX = Input.acceleration.x;
        initAccelY = Input.acceleration.y;
    }

    private void Update()
    {

        

        if (Input.GetButton("Fire1") && Time.time > nextFire && !isHot)
        {
            foreach (var shotSpawn in shotSpawns)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                audioSource.Play();
                weaponHeat++;
                slider.value++;
                
            }

            if(weaponHeat == weaponMAXHeat)
            {
                StartCoroutine(WeaponCooldown());
            }

            
        }

    }

    private IEnumerator WeaponCooldown()
    {
        isHot = true;

        yield return new WaitForSeconds(1);

        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
            weaponHeat = weaponHeat - 2;
            slider.value = slider.value - 2;
        }

        isHot = false;
    }


    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //rb.velocity = movement * speed;

        //rb.position = new Vector3(
        //    Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
        //    0.0f,
        //    Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        //    );
        //rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);


        float moveHorizontalGyro = Input.acceleration.x;
        float moveVerticalGyro = Input.acceleration.y - initAccelY;

        if (moveVerticalGyro < 0)
        {
            moveVerticalGyro = moveVerticalGyro * 5;
        }

        Vector3 movementGyro = new Vector3(moveHorizontalGyro, 0.0f, moveVerticalGyro);
        rb.velocity = movementGyro * speed;

        rb.position = new Vector3(
           Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
           0.0f,
           Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
           );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);






        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

        //    switch (touch.phase)
        //    {
        //        case TouchPhase.Began:
        //            deltaX = touchPos.x - transform.position.x;
        //            deltaY = touchPos.y - transform.position.y;
        //            deltaZ = touchPos.z - transform.position.z;
        //            break;

        //        case TouchPhase.Moved:
        //            rb.MovePosition(new Vector3(touchPos.x - deltaX, touchPos.y - deltaY, touchPos.z - deltaZ));
        //            break;

        //        case TouchPhase.Ended:
        //            rb.velocity = Vector3.zero;
        //            break;

        //    }
        //}
        // rb.position = new Vector3(
        //Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
        //0.0f,
        //Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        //);
        // rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }



 
}

