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

    public bool SeePlayer()
    {
        #region clipping

        Physics.SyncTransforms();
        Bounds bounds = player.bounds;
        Vector3 orig = _transform.position;
        Vector3 closestPt = bounds.ClosestPoint(orig);
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
                    if (cosBetweenTwoVectors(_transform.forward, vectToCastTarget) < Mathf.Cos(angle))
                    {
                        continue;
                    }

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