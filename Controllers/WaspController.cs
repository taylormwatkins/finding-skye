using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspController : MonoBehaviour
{
    // variables for the three birds
    private GameObject Ace;
    private GameObject Ozzy;
    private GameObject Chip;

    // variable for the active player
    private GameObject player;

    // get animator
    private Animator anim;

    // variable for speed
    private float speed;

    // variables for distance and direction
    private float direction;
    private Vector3 difference;
    private float xDifference;
    private float yDifference;

    // game manager reference
    private GameManager gm;

    // can the wasp move?
    private bool canMove;

    // is the wasp actively following the player?
    private bool following;

    // variables for boomerand and chipAttack object
    private Boomerang boomerang;
    private ChipAttack chipAttack;

    // variables for attack cooldown and timer
    private float attackCooldown;
    private float cooldownTimer;


    void Start()
    {
        // set speed and attack cooldown values
        speed = 2;
        attackCooldown = 1f;

        // find the game objects declared above
        Ace = GameObject.Find("Ace");
        Ozzy = GameObject.Find("Ozzy");
        Chip = GameObject.Find("Chip");
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        // can move from the start
        canMove = true;
    }

    void Update()
    {

        cooldownTimer -= Time.deltaTime;

        // find active player
        int playerID = gm.GetActivePlayerID();
        if (playerID == 1)
        {
            player = Ace;
        }
        else if (playerID == 2)
        {
            player = Ozzy;
        }
        else if (playerID == 3)
        {
            player = Chip;

            // find chipAttack object 
            // chip is the only one who can hurt us
            chipAttack = GameObject.Find("Chip").GetComponent<ChipAttack>();

            // if chip's boomerang has been deployed, find it
            if (!chipAttack.HasBoomerang())
            {
                boomerang = GameObject.Find("Boomerang").GetComponent<Boomerang>();
            }
        }

        // find distance from active player
        xDifference = transform.position.x - player.transform.position.x;
        yDifference = transform.position.y - player.transform.position.y;

        // if they're close enough, follow
        if ((xDifference < 8) && (yDifference < 8))
            following = true;

        // get players position, move toward it 
        if (following && canMove)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            direction = transform.position.x - player.transform.position.x;
        }

        // determining which direction to face depending on direction of movement
        if (direction > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        // die if hit by boomerang
        if (collider.gameObject.tag == "Boomerang")
        {
            anim.SetTrigger("Dead");
            canMove = false;
            Invoke("Die", 0.5f);
        }

        // attack when touching the player
        else if (collider.gameObject.tag == "Player" && cooldownTimer <= 0)
        {
            anim.SetTrigger("Attack");
            cooldownTimer = attackCooldown;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // send boomerang home after being hit
        if (collider.gameObject.tag == "Boomerang")
        {
            boomerang.GoHome();
        }

    }

    // destroy wasp object and give player points
    void Die()
    {
        gm.AddScore(300);
        PlayerPrefs.SetInt("wasps", PlayerPrefs.GetInt("wasps") + 1);
        Destroy(gameObject);
    }

}
