using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fish : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float acceleration = 1.0f;
    [SerializeField] private float minInterval = 1;
    [SerializeField] private float maxInterval = 8;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string targetTag = "Food";
    [SerializeField] private int changeDirectionMin = 1;
    [SerializeField] private int changeDirectionMax = 7;
    [SerializeField] private int stomachSize = 50;
    [SerializeField] private float hungerThreshold = 0.8f;
    [SerializeField] private int movementCost = 1;
    [SerializeField] private int foodBonus = 10;
    [SerializeField] private float moneyFrequency = 30;
    [SerializeField] private GameObject money;


    private float changeDirectionInterval;
    private Vector2 movementDirection;
    private float timeSinceLastDirectionChange;
    private Rigidbody2D rb;
    private float closestDistance = Mathf.Infinity;
    private int stomachHunger;
    private float moneyTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementDirection = Random.insideUnitCircle;
        changeDirectionInterval = 0;
        stomachHunger = 30;
        moneyTimer = Time.time;
    }

    void Update()
    {
        catchFood();
        limitSpeed();
        if(stomachSize > 0 && (Time.time - moneyTimer) >= moneyFrequency)
        {
            Instantiate(money, transform.position, Quaternion.identity);
            moneyTimer = Time.time;
        }
        else if(stomachSize < 0)
        {
            print(Time.time - moneyTimer);
            moneyTimer = Time.time;
        }
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
        if (stomachHunger > 0)
        {
            stomachHunger -= movementCost;
        }
    }

    void limitSpeed()
    {
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
    void directionSwitch()
    {
        if (Random.Range(1, changeDirectionMax) == changeDirectionMin)
        {
            movementDirection = Random.insideUnitCircle;
        }
    }

    void catchFood()
    {
        timeSinceLastDirectionChange += Time.deltaTime;
        GameObject[] findFood = GameObject.FindGameObjectsWithTag(targetTag);
        Vector2 closestVector = new Vector2(0, 0);
        float randomInterval = Random.Range(minInterval, maxInterval);

        if (findFood.Length != 0 && stomachHunger < (hungerThreshold * stomachSize))
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
                        print(stomachHunger + "/" + stomachSize);
                        Destroy(obj.gameObject);
                        if ((stomachHunger + foodBonus) > stomachSize)
                        {
                            stomachHunger = stomachSize;
                        }
                        else
                        {
                            stomachHunger += foodBonus;
                        }
                    }
                }
            }
        }
        else if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            directionSwitch();
            swim(randomInterval, movementDirection); ;
        }
    }
}