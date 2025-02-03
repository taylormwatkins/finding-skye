using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables for speed, jump force, damage cooldown
    private float speed;
    private float jumpForce;
    private float damageCooldown;
    private float cooldownTimer = Mathf.Infinity;
    private float horizontalInput;

    // variables for animator and rigidbody
    private Rigidbody2D body;
    private Animator anim;

    // are we on the ground?
    private bool grounded;

    // are we alive?
    private bool alive;

    // storing the active player
    private int activePlayerID;

    // variables for game and audio managers, ozzyAttack, and chipAttack objets
    private GameManager gm;
    private AudioManager am;
    private OzzyAttack ozzyAttack;
    private ChipAttack chipAttack;



    void Start()
    {
        // find the game objects mentioned above
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        horizontalInput = Input.GetAxis("Horizontal");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // always alive at the start
        alive = true;

        // declare cooldown values and jump force
        damageCooldown = 1f;
        cooldownTimer = 0;
        jumpForce = 13.5f;
    }

    private void Update()
    {

        cooldownTimer -= Time.deltaTime;

        // determine active player
        activePlayerID = gm.GetActivePlayerID();

        // get correct speed for active player
        SetSpeed(activePlayerID);

        // get input from left and right keys
        horizontalInput = Input.GetAxis("Horizontal");

        // only move if chip has boomerang and we aren't dead
        if (gm.ChipHasBoomerang() && alive)
        {
            // move the body
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            // flip player when facing left or right
            if (horizontalInput > 0.01f)
            {
                transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
        }

        // jump if we aren't already in the air and chip has her boomerang
        if (Input.GetKey(KeyCode.Space) && grounded && gm.ChipHasBoomerang())
        {
            Jump(jumpForce);
        }

        // if ace is active
        if (activePlayerID == 1)
        {
            // she can do her high jump if she isn't already in the air
            if (Input.GetKey(KeyCode.LeftShift) && grounded)
            {
                Jump(20.0f);
            }
        }

        // set animator parameters
        anim.SetBool("Walk", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);

        // falling to your death
        if (transform.position.y < -5)
        {
            alive = false;
            am.PlaySFX(am.death);
            Death();
        }
    }

    // jumping
    private void Jump(float force)
    {
        // play sound, move character, trigger animation, and !grounded
        am.PlaySFX(am.jump);
        body.velocity = new Vector2(body.velocity.x, force);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // move to next level if colliding with next level object
        if (collision.gameObject.tag == "Next Level")
        {
            gm.NextLevel();
        }

        // grounded if on the ground
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        if (!gm.OzzyIsDashing() && gm.ChipHasBoomerang())
        {
            // only hurt by bullets if ozzy isn't dashing and chip has her boomerang
            if (collision.gameObject.tag == "Bullet")
            {
                Damage();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // !grounded if you leave the ground
        if (collision.gameObject.tag == "Ground")
            grounded = false;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        // this is a bug that needs to be fixed
        // if damage is allowed when chip doesn't have the boomerang
        // the boomerang takes damage
        if (gm.ChipHasBoomerang())
        {
            // take damage if you bump into a porcupine or wasp
            if (collider.gameObject.tag == "Porcupine")
            {
                Damage();
            }

            else if (collider.gameObject.tag == "Wasp")
            {
                Invoke("Damage", 0.3f);
            }
        }
    }

    public void SetSpeed(int player)
    {
        // Ace is faster
        if (player == 1)
        {
            speed = 13.0f;
        }
        // ozzy and chip speed
        else
        {
            speed = 10.0f;
        }

    }

    public void Damage()
    {
        // don't take damage if cooldown timer isn't done 
        if (cooldownTimer <= 0)
        {
            // if this is the last damage to be taken
            if (gm.GetComponent<Health>().GetHealth() == 1f)
            {
                // take the damage
                gm.GetComponent<Health>().TakeDamage();

                // then die
                alive = false;
                anim.SetTrigger("Death");
                am.PlaySFX(am.death);

                
                // wait a beat so death animaton can play
                Invoke("Death", 1f);
            }

            // if this isn't the kill shot
            else
            {
                // play damage sound and damage animation
                am.PlaySFX(am.hit);
                anim.SetTrigger("Damage");

                // take the damage and start the cooldownTimer
                gm.GetComponent<Health>().TakeDamage();
                cooldownTimer = damageCooldown;
            }
        }
    }

    // set game object as inactive and call death method from gm
    public void Death()
    {
        gm.PlayerDied();
        this.gameObject.SetActive(false);
    }
}
