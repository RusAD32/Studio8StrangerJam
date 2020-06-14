using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialugue : MonoBehaviour
{
    public GameObject dialogue;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogue.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            dialogue.SetActive(false);
            Destroy(gameObject);
        }
    }
}
