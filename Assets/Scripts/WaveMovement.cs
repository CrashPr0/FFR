using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float waveFrequency = 1f;
    public float minAmplitude = 0.1f;
    public float maxAmplitude = 1f;
    private Renderer rend;
    private Vector3 startPosition;
    private float waveAmplitude;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startPosition = transform.position;
        waveAmplitude = Random.Range(minAmplitude, maxAmplitude);
    }

    void Update()
    {
        float xOffset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(xOffset, 0));

        float yOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}