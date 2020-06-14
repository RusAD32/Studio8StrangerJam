using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject lightBulb;
    public AudioSource audioS;

    void Start()
    {
        lightBulb.SetActive(false);
        audioS.Stop();
    }

    void OnTriggerEnter()
    {
        lightBulb.SetActive(true);
        Destroy(gameObject, 1f);
        audioS.Play();
    }
}
