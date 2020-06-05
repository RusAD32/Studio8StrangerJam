using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totems : MonoBehaviour
{
    public bool isTaken = false;

    void Start()
    {
        isTaken = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            isTaken = true;
        }
    }
}
