using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float sensitivity = 100f;

    public Transform playerBody;

    public CharacterController controller;

    private float _xRot = 0f;
    private float _vertVel = 0f;
    private float _termVel = 20f;
    private float _grav = 6f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);
        
        _xRot = Mathf.Clamp(_xRot - mouseY, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_xRot, 0, 0);

        float hMovement = Input.GetAxis("Horizontal") * Time.deltaTime * 10;
        float vMovement = Input.GetAxis("Vertical") * Time.deltaTime * 10;
        Vector3 move = playerBody.forward * vMovement + playerBody.right * hMovement;
        bool jump = Input.GetKey(KeyCode.Space);
        if (jump && controller.isGrounded)
        {
            _vertVel = 1.5f;
        }

        _vertVel = Mathf.Min(_vertVel - _grav * Time.deltaTime, _termVel);
        move += Vector3.up * _vertVel;
        controller.Move(move);
    }
}