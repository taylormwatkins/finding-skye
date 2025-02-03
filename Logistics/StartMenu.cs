using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    // declare variables for the different screens
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject startScreen;
    private AudioManager am;



    // Start is called before the first frame update
    void Start()
    {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();


        // start the start screen active and options inactive
        MenuButton();
    }

    // set options to active and start menu to inactive
    public void OptionsButton()
    {
        am.PlaySFX(am.buttonClick);
        startScreen.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // set start menu to active and options to inactive
    public void MenuButton()
    {
        optionsMenu.SetActive(false);
        startScreen.SetActive(true);
    }

}
