using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenText : MonoBehaviour
{
    // where we're putting the text
    public Text textBox;
    
    // the sentence(s) to be displayed
    private string sentence;
    private string[] sentences;
    private int index;
    private bool active = true;

    public float typingSpeed;

    public float duration;
    
    
    IEnumerator Type()
    {
        textBox.text = "";
        foreach (var letter in sentences[index].ToCharArray())
        {
            if (active)
            {
                textBox.text += letter;
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
            
            else
            {
                textBox.text = sentences[index];
            }

        }

    }
    

    IEnumerator TypeMany()
    {
        for (int i = 0; i < sentences.Length+1; i++)
        {
            
            if (index < sentences.Length)
            {
                StartCoroutine(Type());
            }            
            yield return new WaitForSecondsRealtime(duration);
            if (index < sentences.Length)
            {
                index++;
            }    
        }
        
    }

    public void Say(string _text, float _duration = 0)
    {
        string[] phrase = { _text};
        sentences = phrase;

        index = 0;
        if (_duration > 0)
        {
            duration = _duration;
        }
        StartCoroutine(Type());
    }
    
    
     public void Clear()
    {
        sentences = null;
        textBox.text = "";
    }
}



