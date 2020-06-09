using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerDetector : MonoBehaviour
{
    [Tooltip("Object to search for")] public Collider player;
    [Tooltip("Distance of the FOV")] public float distance = 100f;

    [SerializeField, Tooltip("Viewcone angle, in radians")] private float _angle = Mathf.PI / 3;

    public float angle
    {
        get => _angle;
        set
        {
            _angle = value;
            _viewAngleCos = Mathf.Cos(value);
        }
    }

    [Tooltip("How many rays per radian to cast (more rays, more precision, less speed)")]
    public float density = 10f;

    private float _viewAngleCos;

    public void Start()
    {
        _viewAngleCos = Mathf.Cos(_angle);
    }
    public bool SeePlayer()
    {
        #region clipping

        Physics.SyncTransforms();
        Bounds bounds = player.bounds;
        Vector3 orig = transform.position;
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
            if (cosBetweenTwoVectors(transform.forward, vectToTarget) < _viewAngleCos)
            {
                return false;
            }

            RaycastHit res;
            if (Physics.Raycast(orig, vectToTarget, out res, dist + diam))
            {
                return res.collider == player;
            }
        }

        #endregion


        #region MainRaycasting

        // scanning vicinity of the player with rays
        // in this case, rather than scanning the bottom-left corner, let's scan the center of the object
        for (var i = 0; i <= raysToCast / 2; i++)
        {
            for (var j = 0; j <= raysToCast / 2; j++)
            {
                for (var k = -1; k <= 1; k += 2)
                {
                    Vector3 up = transform.up;
                    Vector3 newRight = Vector3.Cross(vectToTarget, up).normalized;
                    Vector3 vectToCastTarget = vectToTarget +
                                               newRight * (i * k * diam) / raysToCast +
                                               up * (j * k * diam) / raysToCast;
                    if (cosBetweenTwoVectors(transform.forward, vectToCastTarget) < _viewAngleCos)
                    {
                        continue;
                    }

                    Debug.DrawRay(orig, vectToCastTarget);
                    if (Physics.Raycast(orig, vectToCastTarget, out RaycastHit res, dist + diam) &&
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