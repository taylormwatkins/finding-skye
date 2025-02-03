using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcupineController : MonoBehaviour
{
    // speed variable
    private float speed;

    // can we move right now?
    private bool canMove;

    // variables for the porcupine's path    
    private Vector3 waypoint;
    private Vector3 target;
    private Vector3 originalPos;

    // porcupine animator
    private Animator anim;

    // variables for game manager, chipAttack and boomerang game objects
    private GameManager gm;
    private Boomerang boomerang;
    private ChipAttack chipAttack;


    // Start is called before the first frame update
    void Start()
    {
        // find animator and game manager
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();


        // set speed
        speed = 3f;

        // remember original position 
        originalPos = transform.position;

        // can move from the start
        canMove = true;

        // find target
        waypoint = originalPos + new Vector3(5f, 0f, 0f);
        target = waypoint;

    }


    void Update()
    {

        // move between waypoints
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        }

        // when waypoint is reached, turn around
        if (transform.position == waypoint)
        {
            target = originalPos;
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }

        // if original position is reached, turn around
        if (transform.position == originalPos)
        {
            target = waypoint;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        }

        // check if chip is active player
        // she's the only one who can hurt us
        if (gm.GetActivePlayerID() == 3)
        {
            chipAttack = GameObject.Find("Chip").GetComponent<ChipAttack>();

            // if chip is active and the boomerang is on the move
            // find boomerang object
            if (!chipAttack.HasBoomerang())
            {
                boomerang = GameObject.Find("Boomerang").GetComponent<Boomerang>();
            }
        }


    }

    // die if hit by a boomerang
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Boomerang")
        {
            anim.SetTrigger("Dead");
            canMove = false;

            // give it a sec so we can send the boomerang home
            Invoke("Die", 0.5f);
        }

    }

    // send boomerang home after being hit
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Boomerang")
        {
            boomerang.GoHome();
        }
    }

    // destroy porcupine and give player points
    void Die()
    {
        gm.AddScore(300);
        PlayerPrefs.SetInt("porcupines", PlayerPrefs.GetInt("porcupines") + 1);
        Destroy(gameObject);
    }
}
