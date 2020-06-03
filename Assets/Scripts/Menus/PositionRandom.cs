using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PositionRandom : MonoBehaviour
{
    public Transform Emitter;
    public ParticleSystem ps;
    private Vector3 startPosition;
    public float duration;
    public float pause;

    private enum state : int
    {
        emitting,
        paused,
    };

    private state cur_state = state.emitting;

    private float _cycleTime;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = Emitter.position;
        ps.Play();
    }

    // Update is called once per frame
    void Update()
    {
        _cycleTime += Time.deltaTime;
        if (cur_state == state.emitting && _cycleTime >= duration)
        {
            _cycleTime = 0f;
            ps.Stop();
            cur_state = state.paused;
        }
        else if (cur_state == state.paused && _cycleTime >= pause)
        {
            Emitter.position = startPosition + new Vector3(1500f * Random.value - 750f, 0f, 0f);
            _cycleTime = 0f;
            ps.Play();
            cur_state = state.emitting;
        }
    }
}