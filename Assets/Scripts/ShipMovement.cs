using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float idleSpeed = 0.1f;
    public float idleAmplitude = 0.1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + new Vector3(0.0f, Mathf.Sin(Time.time * idleSpeed) * idleAmplitude, 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Sin(Time.time * idleSpeed) * idleAmplitude * 10.0f);
    }
}
