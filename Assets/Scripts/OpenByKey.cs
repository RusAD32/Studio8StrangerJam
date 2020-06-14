using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenByKey : MonoBehaviour
{
    public Key key;
    public float timeToDestroyCube = 0.1f;
    public float timeToDestroyAll = 1f;
    public GameObject cube;

    //messages
    public GameObject DoorIsOpened;
    public GameObject DoorIsClosed;

    void Start()
    {
        DoorIsOpened.SetActive(false);
        DoorIsClosed.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && key.keyIsTaken)
        {
            DoorIsOpened.SetActive(true);
            Destroy(cube, timeToDestroyCube);
            Destroy(gameObject, timeToDestroyAll);
        }
        else if(other.gameObject.tag == "Player" && !key.keyIsTaken)
        {
            DoorIsClosed.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoorIsClosed.SetActive(false);
            DoorIsOpened.SetActive(false);
        }
    }
}
