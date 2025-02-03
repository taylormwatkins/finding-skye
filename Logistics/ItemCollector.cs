using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollector : MonoBehaviour
{
    // declare point values
    private int goldPoints = 100;
    private int purplePoints = 200;
    private int healthPoints = 300;


    private GameManager gm;
    private AudioManager am;


    void Start()
    {
        // find the game and audio managers
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }


    // adding the correct points per collectable
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoldCoin"))
        {
            am.PlaySFX(am.coinCollect);
            Destroy(collision.gameObject);
            gm.AddScore(goldPoints);
            PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + 1);

        }
        else if (collision.gameObject.CompareTag("PurpleCoin"))
        {
            am.PlaySFX(am.coinCollect);
            Destroy(collision.gameObject);
            gm.AddScore(purplePoints);
            PlayerPrefs.SetInt("purple", PlayerPrefs.GetInt("purple") + 1);

        }
        // also restore health when health object is collected
        else if (collision.gameObject.CompareTag("Health"))
        {
            am.PlaySFX(am.healthCollect);
            Destroy(collision.gameObject);
            gm.GetComponent<Health>().RestoreHealth();
            gm.AddScore(healthPoints);
            PlayerPrefs.SetInt("fruit", PlayerPrefs.GetInt("fruit") + 1);

        }
    }

}
