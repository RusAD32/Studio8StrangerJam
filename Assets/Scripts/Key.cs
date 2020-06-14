using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool keyIsTaken;
    public float timeToDestroy = 0.2f;

    public AudioSource tick;

    void Start()
    {
        keyIsTaken = false;
        tick.Stop();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            keyIsTaken = true;
            tick.Play();
            Destroy(gameObject, timeToDestroy);
        }
    }
}
