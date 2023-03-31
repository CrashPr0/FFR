using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                //GameManager.instance.NoteHit();
                if (Mathf.Abs(transform.position.y) > 0.25)     
                {
                       GameManager.instance.NormalHit();
                } else if(Mathf.Abs(transform.position.y) > .05f){
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                }
      
            }
        }
    }
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator") {
            canBePressed = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
        }
    }
}
