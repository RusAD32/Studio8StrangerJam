using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShaker : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform anchor;
    private Vector3 _basicOffset;
    public float magnitude = 0;
    
    void Start()
    {
        _basicOffset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = anchor.localPosition + _basicOffset + (Vector3)Random.insideUnitCircle * magnitude;
    }
}
