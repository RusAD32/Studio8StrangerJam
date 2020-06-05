using System;
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
    
    public bool SeePlayer()
    {
        Physics.SyncTransforms();
        Vector3 orig = _transform.position;
        if (distance * distance < player.bounds.ClosestPoint(orig).sqrMagnitude)
        {
            return false;
        }
        var bounds = player.bounds;
        Vector3 targetCenter = bounds.center;
        Vector3 targetSize = bounds.size;
        // length of the diagonal of the bounding box
        float diam = targetSize.magnitude;
        Vector3 vectToTarget = targetCenter - orig;
        // let's save some computational cycles
        // if the target is too far away, don't check anything more
        float dist = player.bounds.ClosestPoint(orig).magnitude;
        // in radians
        float angularDiamApprox = 2f * Mathf.Atan(0.5f * diam / dist);
        // per one dimension
        int raysToCast = (int) Mathf.Ceil(angularDiamApprox * density);
        // in this case, rather than scanning the bottom-left corner, let's scan the center of the object 
        if (raysToCast == 1)
        {
            // if it's outside of our FOV, throw it out
            if (cosBetweenTwoVectors(_transform.forward, vectToTarget) < Mathf.Cos(angle))
            {
                return false;
            }

            if (Physics.Raycast(orig, vectToTarget, out RaycastHit res, dist + diam))
            {
                return res.collider == player;
            }

        }
        Vector3 targetSpherical = cartesianToSpherical(vectToTarget);
        for (var i = 0; i < raysToCast; i++)
        {
            for (var j = 0; j < raysToCast; j++)
            {
                float newPhi = targetSpherical.z - angularDiamApprox / 2 + angularDiamApprox * i / raysToCast;
                float newTheta = targetSpherical.y - angularDiamApprox / 2 + angularDiamApprox * j / raysToCast;
                // ignore it if it's definitely outside the field of view
                Vector3 vectToCastTarget = sphericalToCartesian(new Vector3(targetSpherical.x, newTheta, newPhi));
                if (Mathf.Abs(cosBetweenTwoVectors(_transform.forward, vectToCastTarget)) < Mathf.Cos(angle))
                {
                    continue;
                }

                if (Physics.Raycast(orig, vectToCastTarget, out RaycastHit res, dist + diam) && res.collider == player)
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

    private Vector3 cartesianToSpherical(Vector3 src)
    {
        // x -> r, y -> theta, z -> phi
        float r = src.magnitude;
        float theta = (float) Math.Acos(src.y / src.magnitude);
        float phi = Mathf.Atan2(src.z, src.x);
        return new Vector3(r, theta, phi);
    }

    private Vector3 sphericalToCartesian(Vector3 src)
    {
        float targetX = src.x * Mathf.Sin(src.y) * Mathf.Cos(src.z);
        float targetZ = src.x * Mathf.Sin(src.y) * Mathf.Sin(src.z);
        float targetY = src.x * Mathf.Cos(src.z);
        return new Vector3(targetX, targetY, targetZ);
    }

}
