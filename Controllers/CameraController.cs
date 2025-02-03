using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // variables to store current player
    private Transform player;
    private int whichBird;


    // variables for three bird transforms
    private Transform ace;
    private Transform ozzy;
    private Transform chip;

    // variable to camera shows more depending on
    // direction the player is moving
    private Vector3 offset;

    // make it a smooth transition
    private float smooth;

    // variables for camera boundaries
    private Vector3 init, minPos, maxPos;

    void Awake()
    {
        // find the character transforms
        ace = GameObject.Find("Ace").GetComponent<Transform>();
        ozzy = GameObject.Find("Ozzy").GetComponent<Transform>();
        chip = GameObject.Find("Chip").GetComponent<Transform>();
    }

    void Start()
    {
        // setting camera boundaries
        init = new Vector3(4.25f, 2, -10);
        player = ozzy;
        smooth = 3;
        minPos = init + new Vector3(0.0f, 0.0f, -10);
        maxPos = init + new Vector3(53, 0.0f, -10);


    }

    private void FixedUpdate()
    {
        // follow the player
        Follow();
    }

    void Follow()
    {
        // determine offset based on direction character is moving
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0.01f)
        {
            offset = new Vector3(4, 0f, 0f);
        }
        else if (horizontalInput < -0.01f)
        {
            offset = new Vector3(-4, 0f, 0f);
        }

        // get player position and target position
        Vector3 playerPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        Vector3 targetPosition = playerPosition + offset + init;

        // confine camera to its bounds
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x),
            Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y),
            Mathf.Clamp(targetPosition.z, minPos.z, maxPos.z)
        );

        // smoothly follow the player
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smooth * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

    // follow the correct character
    public void SetNextPlayer()
    {
        whichBird = GameObject.Find("GameManager").GetComponent<GameManager>().GetActivePlayerID();

        if (whichBird == 1)
        {
            player = ace;
        }
        else if (whichBird == 2)
        {
            player = ozzy;
        }
        else if (whichBird == 3)
        {
            player = chip;
        }
    }


}
