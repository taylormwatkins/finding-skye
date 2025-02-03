using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{   
    // how fast can she dash?
    private float dashingPower;

    // variables for target pos and ozzyAttack object
    private Vector3 target;
    private OzzyAttack ozzyAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        // set the speed
        dashingPower = 50f;
    }
    void OnEnable()
    {    
        // ozzy facing right 
        if (transform.parent.localScale.x < 0)
        {
            target = transform.position + new Vector3(5f, 0f, 0f);
        }
        // ozzy facing left
        else
        {
            target = transform.position + new Vector3(-5f, 0f, 0f);
        }
    }

    // go to target
    void Update()
    {
        transform.parent.position = Vector2.MoveTowards(this.transform.parent.position, target, dashingPower * Time.deltaTime);
    }

    // stop dashing
    public void StopDash()
    {
        gameObject.SetActive(false);
    }
}
