using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// we have a separate ozzyAttack object because 
// ozzy is the only one who can dash
// and we don't want any confusion 
public class OzzyAttack : MonoBehaviour
{
    // find playercontroller
    private PlayerController playerController;

    // is she dashing?
    private bool isDashing;

    // variables for dashing timer and cooldown
    private float dashingTime;
    private float dashingCooldown;
    private float cooldownTimer;
    private float dashingTimer;

    // find dash and trail objects
    private GameObject dash;
    private GameObject trail;
    private AudioManager am;


    // Start is called before the first frame update
    void Start()
    {
        // set dash timer and cooldown timer
        dashingTime = 0.2f;
        dashingCooldown = 0.5f;


        // find the gameObjects declared above
        // set dash and trail to inactive
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        playerController = GetComponent<PlayerController>();
        dash = GameObject.Find("Dash");
        dash.SetActive(false);
        trail = GameObject.Find("Trail");
        trail.SetActive(false);

        // not dashing from the start
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        // dash 
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer <= 0 && !isDashing)
        {
            // play sound, start timer and cooldown timer
            dash.SetActive(true);
            am.PlaySFX(am.dash);
            dashingTimer = dashingTime;
            cooldownTimer = dashingCooldown;
            isDashing = true;
            trail.SetActive(true);
        }

        dashingTimer -= Time.deltaTime;
        cooldownTimer -= Time.deltaTime;

        // done dashing
        if (dashingTimer <= 0)
        {
            dash.SetActive(false);
            trail.SetActive(false);
        }

        // cooldown timer complete, she can dash again now
        if (cooldownTimer <= 0)
        {
            isDashing = false;
        }

    }

    // is she dashing right now?
    // this lets us find out from other scripts
    public bool IsDashing()
    {
        return isDashing;
    }

    // be still
    public void StopDashing()
    {
        dashingTimer = 0;
    }
}
