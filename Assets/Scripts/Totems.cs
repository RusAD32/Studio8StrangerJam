using System.Collections;
using UnityEngine;

public class Totems : MonoBehaviour
{
    public bool isTaken;
    public float timeToDestroy = 0.2f;

    public AudioSource tick;

    void Start()
    {
        isTaken = false;
        tick.Stop();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isTaken = true;
            tick.Play();
            Destroy(gameObject, timeToDestroy);
        }
    }
}
