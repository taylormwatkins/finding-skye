using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // declare variables
    private Map map;
    private GameManager gm;
    private AudioManager am;
    private OverlayManager om;
   


    // Start is called before the first frame update
    void Start()
    {
        // find the things
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        om = GameObject.Find("GameManager").GetComponent<OverlayManager>();


        map = gm.GetComponent<Map>();
    }

    // player goes left
    public void LeftPressed()
    {
        am.PlaySFX(am.buttonClick);
        SceneManager.LoadScene(map.WhichWay(SceneManager.GetActiveScene().buildIndex, true));
    }

    // player goes right
    public void RightPressed()
    {
        am.PlaySFX(am.buttonClick);
        SceneManager.LoadScene(map.WhichWay(SceneManager.GetActiveScene().buildIndex, false));
    }

    // restart level button
    public void TryAgain()
    {
        am.PlaySFX(am.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // return to main menu
    public void BackToMenu()
    {
        am.PlaySFX(am.buttonClick);
        PlayerPrefs.SetInt("totalScore", 0);
        PlayerPrefs.SetFloat("health", 3);
        SceneManager.LoadScene(0);
    }

    // start the game
    public void StartButton()
    {
        am.PlaySFX(am.buttonClick);
        SceneManager.LoadScene(1);
    }

    // skip the intro, go straight to level 1
    public void SkipIntro()
    {
        SceneManager.LoadScene(4);
    }

    // resume the game()
    public void ResumeButton()
    {
        am.PlaySFX(am.buttonClick);
        gm.ResumeGame();
    }

    // open options menu
    public void OptionsButton()
    {
        am.PlaySFX(am.buttonClick);
        om.OpenOptions();
    }

    // back to pause menu
    public void Back()
    {
        am.PlaySFX(am.buttonClick);
        om.CloseOptions();
    }

    // exit the game
    public void ExitGame()
    {
        am.PlaySFX(am.buttonClick);
        PlayerPrefs.SetInt("totalScore", 0);
        PlayerPrefs.SetFloat("health", 3);
        Application.Quit();
    }

    // dummy button for testing sfx volume
    public void DummyButton()
    {
        am.PlaySFX(am.buttonClick);
    }
}
