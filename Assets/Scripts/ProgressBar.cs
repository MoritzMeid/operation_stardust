using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{

    private Slider slider;
    int impWeaponHeat;



    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {

        GameObject player = GameObject.Find("Player");
        PlayerController playerScript = player.GetComponent<PlayerController>();
        playerScript.weaponHeat = impWeaponHeat;
        if(slider.value < impWeaponHeat)
        {
            slider.value++;
        }

    }

}
