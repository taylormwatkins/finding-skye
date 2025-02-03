using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    // variable for animator
    private Animator anim;

    // offset slightly from snail's mouth
    private Vector3 bulletOffset = new Vector3(0.5f, 0, 0);

    // store the bullet prefab
    public GameObject bulletPrefab;

    // variable for delay and cooldown timer
    private float fireDelay = 0.75f;
    private float cooldownTimer = 0;

    // variables for game manager and ozzyAttack object
    private GameManager gm;
    private OzzyAttack ozzyAttack;


    void Start()
    {
        // find animator and game manager
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        // check if ozzy is active player
        // she's the only one who can hurt us
        if (gm.GetActivePlayerID() == 2)
        {
            ozzyAttack = GameObject.Find("Ozzy").GetComponent<OzzyAttack>();
        }

        cooldownTimer -= Time.deltaTime;

        // shoot bullets continuously
        if (cooldownTimer <= 0)
        {
            cooldownTimer = fireDelay;

            Vector3 offset = transform.rotation * bulletOffset;

            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation, gameObject.transform);
        }
    }

    // die if hit with dash
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Dash")
        {
            anim.SetTrigger("Dead");
            Invoke("Die", 0.5f);
            ozzyAttack.StopDashing();
        }

    }

    // destroy the snail and give player points
    void Die()
    {
        gm.AddScore(300);
        PlayerPrefs.SetInt("snails", PlayerPrefs.GetInt("snails") + 1);
        Destroy(gameObject);
    }


}

