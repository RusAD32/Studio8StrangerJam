using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightToggle : MonoBehaviour
{
    //flashLight
    public GameObject lighth;

    //bools
    public bool isOpened;
    public AudioClip open;
    public AudioClip closed;

    //battery
    public float battery = 10;

    //SFXs
    public AudioSource noBatterySound;

    void Start()
    {
        isOpened = false;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            isOpened = true;
        }
        else
        {
            isOpened = false;
        }

        if (isOpened)
        {
            noBatterySound.Stop();
            lighth.SetActive(true);
            battery -= 0.5f * Time.deltaTime;
        }
        else
        {
            lighth.SetActive(false);
        }

        if (battery <= 0)
        {
            lighth.SetActive(false);
            battery = 0f;
            noBatterySound.Play();
        }
        else if (battery > 0)
        {
            noBatterySound.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Battery")
        {
            Destroy(other.gameObject);
            battery = 10f;
        }
    }
}