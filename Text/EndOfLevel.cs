using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    // find OnScreenText to display the text
    public OnScreenText onScreen;

    // text detailing the statistics for the level 
    public string levelStats = "";

    // variables for number of coins and enemies
    int purple;
    int gold;
    int fruit;
    int porcupines;
    int snails;
    int wasps;


    private void Start()
    {
        // how many of each are in this level
        // make sure these are counted at the beginning because they get destroyed at we go
        purple = GameObject.FindGameObjectsWithTag("PurpleCoin").Length;
        gold = GameObject.FindGameObjectsWithTag("GoldCoin").Length;
        fruit = GameObject.FindGameObjectsWithTag("Health").Length;
        porcupines = GameObject.FindGameObjectsWithTag("Porcupine").Length;
        snails = GameObject.FindGameObjectsWithTag("Snail").Length;
        wasps = GameObject.FindGameObjectsWithTag("Wasp").Length;

    }

    public void GetScoreStats()
    {
        if (purple > 0)
        {
            levelStats += "Purple coins: " + PlayerPrefs.GetInt("purple") + "/" + purple + "\n";
        }
        if (gold > 0)
        {
            levelStats += "Gold coins: " + PlayerPrefs.GetInt("gold") + "/" + gold + "\n";
        }
        if (fruit > 0)
        {
            levelStats += "Fruit: " + PlayerPrefs.GetInt("fruit") + "/" + fruit + "\n";
        }
        if (porcupines > 0)
        {
            levelStats += "Porcupines: " + PlayerPrefs.GetInt("porcupines") + "/" + porcupines + "\n";
        }
        if (snails > 0)
        {
            levelStats += "Snails: " + PlayerPrefs.GetInt("snails") + "/" + snails + "\n";
        }
        if (wasps > 0)
        {
            levelStats += "Wasps: " + PlayerPrefs.GetInt("wasps") + "/" + wasps + "\n";
        }

    }

    // display the text
    public void Display()
    {
        GetScoreStats();
        onScreen.Say(levelStats);
    }

}
