using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float blinkInterval = 2f;
    [SerializeField] private float blinkTimes;
    [SerializeField] private float blinkMax = 5f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        blinkTimes = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude == 0)
        {
            StartCoroutine(BlinkAndDestroyCoroutine());
        }
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    IEnumerator BlinkAndDestroyCoroutine()
    {
        if (blinkTimes <= blinkMax)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            blinkTimes += 1f;
        }
        else
        {
            Destroy(gameObject);
        }
    
    }

}
