using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fish : MonoBehaviour
{
    public float speed = 1.0f;
    public float acceleration = 1.0f;
    public float minInterval = 1;
    public float maxInterval = 8;
    public SpriteRenderer spriteRenderer;
    public string targetTag = "Food";

    private float changeDirectionInterval;
    private Vector2 movementDirection;
    private float timeSinceLastDirectionChange;
    private Rigidbody2D rb;

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
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector2 closestVector = new Vector2(0,0);

        if(findFood.Length != 0)
        {
            foreach (GameObject obj in findFood)
            {
                Vector2 distance = transform.position - obj.transform.position;

                if (distance.magnitude < closestDistance)
                {
                    print(distance.magnitude + " , " + closestDistance);
                    closestDistance = distance.magnitude;
                    closestVector = transform.position - obj.transform.position;
                    closestTarget = obj;
                }

                if (closestDistance < 10 && timeSinceLastDirectionChange >= changeDirectionInterval)
                {
                    flip(movementDirection);
                    rb.AddForce(movementDirection * acceleration * 100);
                    changeDirectionInterval = minInterval;
                    movementDirection = -closestVector.normalized;
                    timeSinceLastDirectionChange = 0.0f;
                    if (closestDistance < 0.75)
                    {
                        Destroy(obj.gameObject);
                    }
                }
            }
        }
        else if(timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            wander();
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

    void wander()
    {
        flip(movementDirection);
        rb.AddForce(movementDirection * acceleration * 100);
        changeDirectionInterval = Random.Range(minInterval, maxInterval);
        movementDirection = Random.insideUnitCircle;
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