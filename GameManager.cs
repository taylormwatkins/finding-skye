using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // variables for player transforms
    private Transform Ace;
    private Transform Ozzy;
    private Transform Chip;

    // variable to keep track of current player
    public int activePlayerID;

    private AudioManager am;
    private OverlayManager om;

    // variable to keep track of player position
    // when switching characters
    private Vector3 currentPos;
    private Vector3 currentScale;

    private Text scoreDisplay;

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        // find the player transforms
        Ace = GameObject.Find("Ace").GetComponent<Transform>();
        Ozzy = GameObject.Find("Ozzy").GetComponent<Transform>();
        Chip = GameObject.Find("Chip").GetComponent<Transform>();

        // set ozzy as active player to start
        SetInactivePlayer(Ace);
        SetInactivePlayer(Chip);
        SetActivePlayer(Ozzy);
        activePlayerID = 2;

        // ensure that game isn't paused
        Time.timeScale = 1;


        // find audio manager, overlay manager, and score display object
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        om = GetComponent<OverlayManager>();

        scoreDisplay = GameObject.Find("Score Display").GetComponent<Text>();


        isPaused = false;

        // set volume preference
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        }


        // reset score and collection stats
        ResetStats();

        // display current score
        DisplayScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {

            // pause if escape is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }

            // switch player if right shift is pressed and no special abilites are active
            if (Input.GetKeyDown(KeyCode.RightShift) && ChipHasBoomerang() && !OzzyIsDashing())
            {
                SwitchCharacter();
            }
        }
    }

    // getter so we can identify the
    // active player from other scripts
    public int GetActivePlayerID()
    {
        return activePlayerID;
    }


    // does chip have her boomerang?
    public bool ChipHasBoomerang()
    {
        if (activePlayerID == 3)
        {
            // find chipAttack object to find out
            ChipAttack chipAttack = GameObject.Find("Chip").GetComponent<ChipAttack>();
            return chipAttack.HasBoomerang();
        }
        else
        {
            return true;
        }

    }

    // is ozzy currently dashing?
    public bool OzzyIsDashing()
    {
        if (activePlayerID == 2)
        {
            // find ozzyAttack object to find out
            OzzyAttack ozzyAttack = GameObject.Find("Ozzy").GetComponent<OzzyAttack>();
            return ozzyAttack.IsDashing();
        }
        else
        {
            return false;
        }
    }

    // switch active character
    // tell the camera who to follow
    public void SwitchCharacter()
    {
        // switch from ace to ozzy
        if (activePlayerID == 1)
        {
            activePlayerID = 2;
            SetInactivePlayer(Ace);
            SetActivePlayer(Ozzy);
            GameObject.Find("Main Camera").GetComponent<CameraController>().SetNextPlayer();
        }
        // switch from ozzy to chip
        else if (activePlayerID == 2)
        {
            activePlayerID = 3;
            SetInactivePlayer(Ozzy);
            SetActivePlayer(Chip);
            GameObject.Find("Main Camera").GetComponent<CameraController>().SetNextPlayer();
        }
        // switch from chip to ace
        else if (activePlayerID == 3)
        {
            activePlayerID = 1;
            SetInactivePlayer(Chip);
            SetActivePlayer(Ace);
            GameObject.Find("Main Camera").GetComponent<CameraController>().SetNextPlayer();
        }
    }

    // set a character inactive, remember their position
    public void SetInactivePlayer(Transform name)
    {
        name.gameObject.SetActive(false);
        currentPos = new Vector3(name.position.x, name.position.y, name.position.z);
        currentScale = new Vector3(name.localScale.x, name.localScale.y, name.localScale.z);
    }

    // set new character active and put them in the correct position
    public void SetActivePlayer(Transform name)
    {
        name.gameObject.SetActive(true);
        name.position = currentPos;
        name.localScale = currentScale;
    }

    // display game over screen and stop time when player dies
    public void PlayerDied()
    {
        activePlayerID = 0;
        om.GameOver();
        StopTime();
        PlayerPrefs.SetInt("totalScore", 0);
        PlayerPrefs.SetFloat("health", 3);
    }

    // display next level screen and stop time when end of level is reached
    public void NextLevel()
    {
        om.NextLevel();
        StopTime();
    }

    // stop time
    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        om.Pause();
        isPaused = true;
        StopTime();
    }

    // resume game
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        om.Resume();
    }

    // add to and display score
    public void AddScore(int add)
    {
        int score = GetScore() + add;
        PlayerPrefs.SetInt("totalScore", score);
        DisplayScore();
    }

    // easy getter
    public int GetScore()
    {
        return PlayerPrefs.GetInt("totalScore");
    }

    public void DisplayScore()
    {
        scoreDisplay.text = "Score: " + PlayerPrefs.GetInt("totalScore");
    }

    public void ResetStats()
    {
        PlayerPrefs.SetInt("totalScore", 0);
        PlayerPrefs.SetInt("purple", 0);
        PlayerPrefs.SetInt("gold", 0);
        PlayerPrefs.SetInt("fruit", 0);
        PlayerPrefs.SetInt("porcupines", 0);
        PlayerPrefs.SetInt("snails", 0);
        PlayerPrefs.SetInt("wasps", 0);
    }

}
