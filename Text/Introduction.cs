using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    // drag the OnScreenText script here in the inspector
    public OnScreenText onScreen;

    // the text you want on screen 
    public string story;

    // waiting a second or two before the text begins
    public float delay;

    // how long until the next scene is called
    public float duration;
    

    private void Start()
    {
        // wait a beat before displaying the text
        Invoke("Story", delay);

        // when text is done, call next scene
        Invoke("NextScene", duration);
    }

    // display the text
    public void Story()
    {
        onScreen.Say(story);
    }

    // call the next scene
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

}
