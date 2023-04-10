using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFeeder : MonoBehaviour
{
    [SerializeField] private GameObject fishFood;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(fishFood, new Vector3(mousePos.x, mousePos.y, 0.1f), Quaternion.identity);
        }
    }
}
