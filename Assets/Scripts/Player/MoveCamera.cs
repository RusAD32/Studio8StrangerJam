using System;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform player;
    public PlayerMovement mov;
    private Vector3 _targetPos;

    private void Start()
    {
        _targetPos = transform.position;
    }

    void Update()
    {
        float speed = mov.maxSpeed;
        _targetPos = player.transform.position;
        // transform.position = _target_pos;
        float curSpeed = speed; //Mathf.Max(speed, (transform.position - _targetPos).magnitude * speed);
        if ((transform.position - _targetPos).magnitude <= curSpeed * Time.deltaTime)
        {
            transform.position = _targetPos;
        }
        else
        {
            Vector3 move = (_targetPos - transform.position).normalized * (curSpeed * Time.deltaTime);
            transform.position = new Vector3(_targetPos.x, transform.position.y + move.y, _targetPos.z);
        }
    }
}