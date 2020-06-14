using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    public Followee followee;
    private const float TOLERANCE = 0.01f;
    private Vector3 _bestCandidate;
    private Vector3 _secondCandidate;
    private Collider _followeeCollider;

    private enum FollowerState
    {
        Normal,
    }

    private NavMeshAgent _agent;
    private FollowerState CurrentState = FollowerState.Normal;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _followeeCollider = followee.GetComponent<Collider>();
    }

    private void Update()
    {
        if (CurrentState == FollowerState.Normal)
        {
            UpdateCandidatePosition();
            if (NavMesh.SamplePosition(_bestCandidate, out var meshHit, 2f, _agent.areaMask))
            {
                _agent.destination = meshHit.position;
            }
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                RotateTowards(followee.transform);
            }
        }
    }

    // Update is called once per frame
    void UpdateCandidatePosition()
    {
        var position = transform.position;
        var prev = _bestCandidate;
        _bestCandidate = followee.FollowerPointA;
        if (Vector3.Distance(transform.position, _bestCandidate) > Vector3.Distance(transform.position, followee.FollowerPointB))
        {
            _secondCandidate = _bestCandidate;
            _bestCandidate = followee.FollowerPointB;

        }
        else
        {
            _secondCandidate = followee.FollowerPointB;
        }

        Transform orient = followee.orientation;
        float targetVectSign =
            Mathf.Sign(mixedProduct(orient.up, orient.forward, position - orient.position));
        if (Math.Abs(Mathf.Sign(mixedProduct(orient.up, orient.forward, _bestCandidate - orient.position)) - targetVectSign) > TOLERANCE)
        {
            var tmp = _bestCandidate;
            _bestCandidate = _secondCandidate;
            _secondCandidate = tmp;
        }
        Debug.DrawRay(Vector3.zero, orient.forward);
        Debug.DrawRay(Vector3.zero, _bestCandidate);
        if (cosBetweenTwoVectors(orient.forward, _bestCandidate) > 0.5)
        {
           _bestCandidate += (transform.position - orient.position) * 0.5f;
        }

    }

    float mixedProduct(Vector3 a, Vector3 b, Vector3 c)
    {
        return Vector3.Dot(Vector3.Cross(a, b), c);
    }
    
    private float cosBetweenTwoVectors(Vector3 a, Vector3 b)
    {
        return (a.x * b.x + a.y * b.y + a.z * b.z) / (a.magnitude * b.magnitude);
    }
    
    private void RotateTowards (Transform target) {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        if (Quaternion.Angle(transform.rotation, lookRotation) < 20f)
        {
            transform.rotation = lookRotation;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
}
