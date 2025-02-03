using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingText : MonoBehaviour
{
    // find OnScreenText to display the text
    public OnScreenText onScreen;

    // in the inspector
    // declare the sentence to be displayed
    public string story;

    // how long before the text is displayed
    public float delay;

    // how long before the text is destroyed
    public float textDuration;

    // how long until next scene is called
    public float duration;
    

    // display the text after a beat
    // destroy the text after another beat
    // call the next scene when ready 
    private void Start()
    {
        Invoke("Story", delay);
        Invoke("DestroyText", textDuration);
        Invoke("NextScene", duration);
    }

    // display on screen text
    public void Story()
    {
        onScreen.Say(story);
    }

    // clear on screen text
    public void DestroyText()
    {
        onScreen.Clear();
    }

    // call the next scene
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

}
