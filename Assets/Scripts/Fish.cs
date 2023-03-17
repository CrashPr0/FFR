using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float acceleration = 1.0f;
    [SerializeField]
    private float minInterval = 1;
    [SerializeField]
    private float maxInterval = 8;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float swimTime = 1.0f;
    [SerializeField]
    private string targetTag = "Food";

    private float changeDirectionInterval;
    private Vector2 movementDirection;
    private float timeSinceLastDirectionChange;
    private Rigidbody2D rb;
    float closestDistance = Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementDirection = Random.insideUnitCircle;
        changeDirectionInterval = 0;
    }

    void Update()
    {
        timeSinceLastDirectionChange += Time.deltaTime;
        GameObject[] findFood = GameObject.FindGameObjectsWithTag(targetTag);
        Vector2 closestVector = new Vector2(0,0);
        float randomInterval = Random.Range(minInterval, maxInterval);
        Vector2 randomDirection = Random.insideUnitCircle;

        if (findFood.Length != 0)
        {
            foreach (GameObject obj in findFood)
            {
                Vector2 distance = transform.position - obj.transform.position;

                if (obj == findFood.Last())
                {
                    closestDistance = Mathf.Infinity;
                }

                if (distance.magnitude < closestDistance)
                {
                    closestDistance = distance.magnitude;
                    closestVector = transform.position - obj.transform.position;
                    
                    if (closestDistance < 10 && timeSinceLastDirectionChange >= changeDirectionInterval)
                    {
                        swim(minInterval, -closestVector.normalized);
                    }
                    if (closestDistance < 0.75)
                    {
                        Destroy(obj.gameObject);
                    }
                }
            }
        }

        else if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            swim(randomInterval, randomDirection);
        }

        limitSpeed();
    }

    void flip(Vector2 direction)
    {
        if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void swim(float interval, Vector2 direction)
    {
        changeDirectionInterval = interval;
        flip(direction);
        rb.AddForce(direction * acceleration * 100);
        timeSinceLastDirectionChange = 0.0f;
    }

    void limitSpeed()
    {
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
}