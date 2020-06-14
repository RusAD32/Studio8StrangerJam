using System;
using UnityEngine;

public class Followee : MonoBehaviour
{
    [NonSerialized]
    public Vector3 FollowerPointA;    
    [NonSerialized]
    public Vector3 FollowerPointB;

    public Transform orientation;

    public float distance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        FollowerPointA = transform.position;
        FollowerPointB = FollowerPointA;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var up = orientation.up;
        Quaternion dir1 = Quaternion.AngleAxis(135, up);
        Quaternion dir2 = Quaternion.AngleAxis(-135, up);
        //Debug.DrawRay(orientation.position, dir1 * orientation.forward * distance);
        int layermask = LayerMask.GetMask("Default", "Ground");
        if (Physics.Raycast(new Ray(orientation.position, dir1 * (orientation.forward - orientation.position) * distance), out var hit, distance, layermask))
        {
            FollowerPointA = hit.point;
        }
        else
        {
            FollowerPointA = orientation.position + dir1 * orientation.forward * distance;
        }
        if (Physics.Raycast(new Ray(orientation.position, dir2 * (orientation.forward - orientation.position) * distance), out var hit2, distance, layermask))
        {
            FollowerPointB = hit2.point;
        }
        else
        {
            FollowerPointB = orientation.position + dir2 * orientation.forward * distance;
        }        
        //Debug.DrawRay(Vector3.zero, FollowerPointA);
        //Debug.DrawRay(Vector3.zero, FollowerPointB);

    }
}
