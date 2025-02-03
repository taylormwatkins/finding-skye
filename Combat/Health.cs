using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // variables for health and damage
    private float startingHealth = 3;
    private float currentHealth;
    private float damage = 1f;

    // variables for health bar sprites
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;



    void Start()
    {

        // if its the first time playing 
        if (!PlayerPrefs.HasKey("health"))
        {
            PlayerPrefs.SetFloat("health", 3);
        }


        currentHealth = PlayerPrefs.GetFloat("health");


        // fill image array with hearts
        for (int i = 0; i < 5; i++)
        {
            hearts[i] = GameObject.Find("Heart (" + i + ")").GetComponent<Image>();
        }
    }


    void FixedUpdate()
    {
        // initially put empty heart sprites up
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }

        // fill array with appropriate number of hearts
        if (currentHealth > 0)
        {

            for (int i = (int)startingHealth; i < 5; i++)
            {
                hearts[i].gameObject.SetActive(false);
            }

            for (int i = 1; i <= currentHealth; i++)
            {
                hearts[i - 1].sprite = fullHeart;
            }
        }

    }

    // player got hit
    public void TakeDamage()
    {
        currentHealth -= damage;
        PlayerPrefs.SetFloat("health", currentHealth);
        Debug.Log("Damage Taken: " + currentHealth);

    }

    // return current health to other scripts
    public float GetHealth()
    {
        return currentHealth;
    }

    // starting health isn't used right now
    // but will be after NPCs are introduced
    public void AddStartingHealth()
    {
        startingHealth = startingHealth + 1;
    }

    // player collected fruit, health goes up
    public void RestoreHealth()
    {
        if (currentHealth < startingHealth)
            currentHealth = currentHealth + 1;
        PlayerPrefs.SetFloat("health", currentHealth);
    }
}
