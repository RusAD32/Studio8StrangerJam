using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Collider player;
    public float distance = 100f;
    public float angle = Mathf.PI / 3;
    public float density = 10f;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
    }

    bool SeePlayer()
    {
        Physics.SyncTransforms();
        var bounds = player.bounds;
        Vector3 targetCenter = bounds.center;
        Vector3 targetSize = bounds.size;
        // lentgh of the diagonal of the bounding box
        float diam = Mathf.Sqrt(targetSize.x * targetSize.x + targetSize.y * targetSize.y + targetSize.z * targetSize.z);
        Vector3 orig = _transform.position;
        Vector3 vectToTarget = targetCenter - orig;
        float dist = player.bounds.ClosestPoint(orig).magnitude;
        // let's save some computational cycles
        if (dist > distance)
        {
            return false;
        }
        // in radians
        float angularDiamApprox = 2f * Mathf.Atan(0.5f * diam / dist);
        // per one dimension
        int raysToCast = (int)Mathf.Ceil(angularDiamApprox * density);
        Vector3 lookVector = _transform.forward.normalized * distance;
        // in this case, rather than scanning the bottom-left corner, let's scan the center of the object 
        if (raysToCast == 1)
        {
            // if it's outside of our FOV, throw it out
            Debug.Log(_transform.forward);
            Debug.Log(vectToTarget);
            Debug.Log(cosBetweenTwoVectors(_transform.forward, vectToTarget));
            Debug.Log("\n");
            if (cosBetweenTwoVectors(_transform.forward, vectToTarget) < Mathf.Cos(angle))
            {
                return false;
            }
            RaycastHit res;
            if (Physics.Raycast(orig, vectToTarget, out res, distance))
            {
                return res.collider == player;
            }
            
        }

        
        float phi = Mathf.Atan2(vectToTarget.z, vectToTarget.x) - angularDiamApprox / 2;
        float theta = (float) Math.Acos(vectToTarget.y / vectToTarget.magnitude) - angularDiamApprox / 2;
        for (var i = 0; i < raysToCast; i++)
        {
            for (var j = 0; j < raysToCast; j++)
            {
                float newPhi = phi + angularDiamApprox * i / raysToCast;
                float newTheta = theta + angularDiamApprox * j / raysToCast;
                // ignore it if it's definitely outside the field of view
                float targetX = vectToTarget.magnitude * Mathf.Sin(newTheta) * Mathf.Cos(newPhi);
                float targetZ = vectToTarget.magnitude * Mathf.Sin(newTheta) * Mathf.Sin(newPhi);
                float targetY = vectToTarget.magnitude * Mathf.Cos(newTheta);
                Vector3 vectToCastTarget = new Vector3(targetX, targetY, targetZ);
                DebugSphere.position = orig + vectToCastTarget;
               // Debug.Log(Mathf.Abs(cosBetweenTwoVectors(_transform.forward, vectToCastTarget)));
                if (Mathf.Abs(cosBetweenTwoVectors(_transform.forward, vectToCastTarget)) < Mathf.Cos(angle))
                {
                    continue;
                }
                RaycastHit res;
                if (Physics.Raycast(orig, vectToCastTarget, out res, distance) && res.collider == player)
                { 
                    return true;
                }
                
            }
        }
        
        return false;
    }

    private float cosBetweenTwoVectors(Vector3 a, Vector3 b)
    {
        return (a.x * b.x + a.y * b.y + a.z * b.z) / (a.magnitude * b.magnitude);
    }
    
    
}
