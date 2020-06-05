using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class rotateNdetect : MonoBehaviour
{
    // Start is called before the first frame update
    private float _spd;
    private PlayerDetector _playerDetector;
    private Material _mat;

    void Start()
    {
        _playerDetector = GetComponent<PlayerDetector>();
        _mat = GetComponent<MeshRenderer>().material;
        _spd = Random.value * 360;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(transform.up, _spd*Time.deltaTime);
        if (_playerDetector.SeePlayer())
        {
            _mat.color = Color.red;
        }
        else
        {
            _mat.color = Color.gray;
        }
    }
}
