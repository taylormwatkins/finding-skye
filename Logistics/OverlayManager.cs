using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{

    // overlay screens
    public GameObject gameOver;
    public GameObject nextLevel;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    public EndOfLevel eol;


    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void NextLevel()
    {
        nextLevel.SetActive(true);
        eol.Display();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
    }

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }


}
