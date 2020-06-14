using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class rotateNdetect : MonoBehaviour
{
    // Start is called before the first frame update
    private float _spd;
    private PlayerDetector _playerDetector;
    private Material _mat;
    private NavMeshAgent _agent;
    void Start()
    {
        _playerDetector = GetComponent<PlayerDetector>();
        _mat = GetComponent<MeshRenderer>().material;
        _spd = Random.value * 360;
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (_playerDetector.SeePlayer())
        {
            _mat.color = Color.red;
            if (_agent != null)
            {
                _agent.destination = _playerDetector.player.ClosestPoint(transform.position);
            }
        }
        else
        {
            transform.Rotate(transform.up, _spd*Time.deltaTime);
            _mat.color = Color.gray;
        }
    }
}
