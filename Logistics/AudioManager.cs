using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip lakeMusic;
    public AudioClip mountainMusic;
    public AudioClip forestMusic;
    public AudioClip buttonClick;
    public AudioClip departBoomerang;
    public AudioClip returnBoomerang;
    public AudioClip coinCollect;
    public AudioClip healthCollect;
    public AudioClip hit;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip death;
    public AudioClip switchCharacter;
    // public AudioClip aceJump;

    // variable to know which level we are on 
    // I'll need to update this as I add more levels
    private int level;

    void Awake() 
    {
        // figure out which level we're on
        level = SceneManager.GetActiveScene().buildIndex;
    }

    void Start() 
    {
        // if start menu, intro, or end scene
        if (level < 3 || level == 11)
        {
            musicSource.clip = background;
            musicSource.Play();
        }
        else if (level == 4 || level == 5 || level == 10)
        {
            musicSource.clip = lakeMusic;
            musicSource.Play();
        }
        else if (level == 6 || level == 7)
        {
            musicSource.clip = mountainMusic;
            musicSource.Play();
        }
        else if (level == 8 || level == 9)
        {
            musicSource.clip = forestMusic;
            musicSource.Play();
        }
      
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
