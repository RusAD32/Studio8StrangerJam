using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerDetector : MonoBehaviour
{
    [Tooltip("Position of the object that should scan for the player")]
    public Transform detector;
    [Tooltip("Object to search for")]
    public Collider player;
    [Tooltip("Distance of the FOV")]
    public float distance = 100f;
    [Tooltip("Angle of the FOV")]
    public float angle = Mathf.PI / 3;
    [Tooltip("How many rays per radian to cast (more rays, more precision, less speed)")]
    public float density = 10f;
    
    public bool SeePlayer()
    {
        #region clipping

        Physics.SyncTransforms();
        Bounds bounds = player.bounds;
        Vector3 orig = detector.position;
        Vector3 closestPt = bounds.ClosestPoint(orig) - orig;
        // let's save some computational cycles
        if (distance * distance < closestPt.sqrMagnitude)
        {
            return false;
        }

        Vector3 targetCenter = bounds.center;
        Vector3 targetSize = bounds.size;
        // lentgh of the diagonal of the bounding box
        float diam = targetSize.magnitude;
        Vector3 vectToTarget = targetCenter - orig;
        float dist = closestPt.magnitude;
        // in radians
        float angularDiamApprox = 2f * Mathf.Atan2(0.5f * diam, dist);
        // per one dimension
        int raysToCast = (int) Mathf.Ceil(angularDiamApprox * density);

        #endregion

        #region tooFewRays

        if (raysToCast == 1)
        {
            // if it's outside of our FOV, throw it out
            if (cosBetweenTwoVectors(detector.forward, vectToTarget) < Mathf.Cos(angle))
            {
                return false;
            }

            RaycastHit res;
            if (Physics.Raycast(orig, vectToTarget, out res, distance))
            {
                return res.collider == player;
            }
        }

        #endregion

        // in this case, rather than scanning the bottom-left corner, let's scan the center of the object 

        #region MainRaycasting

        float phi = Mathf.Atan2(vectToTarget.z, vectToTarget.x);
        float theta = (float) Math.Acos(vectToTarget.y / vectToTarget.magnitude);
        for (var i = 0; i <= raysToCast / 2; i++)
        {
            for (var j = 0; j <= raysToCast / 2; j++)
            {
                for (var k = -1; k <= 1; k += 2)
                {
                    float newPhi = phi + angularDiamApprox * i / raysToCast;
                    float newTheta = theta + angularDiamApprox * j / raysToCast;
                    // ignore it if it's definitely outside the field of view
                    float targetX = vectToTarget.magnitude * Mathf.Sin(newTheta) * Mathf.Cos(newPhi);
                    float targetZ = vectToTarget.magnitude * Mathf.Sin(newTheta) * Mathf.Sin(newPhi);
                    float targetY = vectToTarget.magnitude * Mathf.Cos(newTheta);
                    Vector3 vectToCastTarget = new Vector3(targetX, targetY, targetZ);
                    if (cosBetweenTwoVectors(detector.forward, vectToCastTarget) < Mathf.Cos(angle))
                    {
                        continue;
                    }
                    Debug.DrawRay(orig, vectToCastTarget);    
                    if (Physics.Raycast(orig, vectToCastTarget, out RaycastHit res, distance) &&
                        res.collider == player)
                    {
                        return true;
                    }
                }
            }
        }

        #endregion

        return false;
    }

    private float cosBetweenTwoVectors(Vector3 a, Vector3 b)
    {
        return (a.x * b.x + a.y * b.y + a.z * b.z) / (a.magnitude * b.magnitude);
    }
}