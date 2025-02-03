using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBehavior : MonoBehaviour
{
    // declare variables
    public float speed;
    public float delay;
    private bool running;
    private bool jumping;
    private Animator anim;
    public Vector3 target;


    // unpause the game
    // go to mom after the delay
    void Start()
    {
        Time.timeScale = 1;
        anim = GetComponent<Animator>();
        running = false;
        Invoke("GoToMom", delay);
    }

    void Update()
    {
        anim.SetBool("Walk", running);
        anim.SetBool("Grounded", running);

        // run to mom 
        if (running)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            if (transform.position == target)
            {
                FoundHer();
            }
        }

        // if you're on the right side of mom, turn around
        if(transform.position.x > 12 && jumping)
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
    }

    // go get her
    public void GoToMom()
    {
        running = true;
    }

    // stop running, happy jump
    public void FoundHer()
    {
        anim.SetTrigger("Jump");
        running = false;
        jumping = true;
    }
}
