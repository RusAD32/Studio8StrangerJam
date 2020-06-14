using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorNoDialogue : MonoBehaviour
{
    public Animator anim;
    public GameObject otherDoor;
    public AudioSource audios;
    public AudioSource audiodos;

    void Start()
    {
        anim.SetBool("isOpened", false);
        anim.SetBool("isClosed", false);
        otherDoor.SetActive(false);
        audios.Stop();
        audiodos.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("isOpened", true);
            otherDoor.SetActive(false);
            audios.Play();
            audiodos.Stop();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("isOpened", false);
            anim.SetBool("isClosed", true);
            audiodos.Play();
            otherDoor.SetActive(true);
            Destroy(gameObject, 0.10f);
        }
    }
}
