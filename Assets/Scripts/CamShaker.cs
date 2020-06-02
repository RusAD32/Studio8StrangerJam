using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShaker : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform anchor;
    private Vector3 _basicOffset;
    private float _magnitude;
    private float _duration;
    private const float INFINITE_DUR = -1.0f;


    void Start()
    {
        this._basicOffset = (transform.position - anchor.position) / anchor.localScale.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = anchor.position + _basicOffset * anchor.localScale.magnitude + Random.insideUnitSphere * _magnitude;
        if (this._duration != INFINITE_DUR)
        {
            this._duration -= Time.deltaTime;
            if (this._duration <= 0f)
            {
                this.StopShaking();
            }
        }
    }

    public void StartShaking(float magnitude)
    {
        this._magnitude = magnitude;
        this._duration = INFINITE_DUR;
    }

    public void StopShaking()
    {
        this._magnitude = 0f;
        this._duration = 0f;
    }

    public void ShakeFor(float magnitude, float duration)
    {
        this._duration = duration;
        this._magnitude = magnitude;
    }
}