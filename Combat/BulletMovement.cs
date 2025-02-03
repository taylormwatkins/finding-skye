using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // variables for speed and duration
    private float maxSpeed = 8f;
    private float timer = 2f;

    // Start is called before the first frame update
    void Update()
    {
        // move the bullet forward
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(maxSpeed * Time.deltaTime, 0, 0);
       
        // determining direction of bullet
        if (gameObject.transform.parent.localScale.x < 0) 
        {
            pos -= transform.rotation * velocity;
        }
        else 
        {
            pos += transform.rotation * velocity;
        }
        
        transform.position = pos;

        // self destruct after a set amount of time
        timer -= Time.deltaTime;
        if(timer <= 0)
            Destroy(gameObject);  
    }

    // destroy early if it collides with the player
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
