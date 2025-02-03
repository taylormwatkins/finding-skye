using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    // set boomerang speed
    private float speed;

    // variables for position of chip and the boomerang
    private Vector3 offset;
    private Vector3 target;
    private Vector3 originalPos;
    private ChipAttack chipAttack;
    private AudioManager am;

    // is the boomerang on its way out or in 
    private bool returning;

    // Start is called before the first frame update
    void Start()
    {
        //set speed
        speed = 10f;
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    void OnEnable()
    {
        // on its way out
        returning = false;

        // find chipAttack and get chip's position
        chipAttack = GameObject.Find("Chip").GetComponent<ChipAttack>();
        originalPos = chipAttack.GetPosition();

        // chip facing right 
        if (transform.parent.localScale.x < 0)
        {
            offset = new Vector3(0.5f, 0, 0);
            transform.position += offset;
            target = originalPos + new Vector3(10f, 0f, 0f);
        }
        // chip facing left
        else
        {
            offset = new Vector3(-0.5f, 0, 0);
            transform.position += offset;
            target = originalPos + new Vector3(-10f, 0f, 0f);
        }
    }


    void Update()
    {
        // when it's on its way out
        // head toward target
        if (!returning)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        }
        // when it's coming back
        // head toward chip
        else if (returning)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, chipAttack.GetPosition(), speed * Time.deltaTime);
        }

        // head home when target is reached
        if (transform.position == target)
        {
            GoHome();
        }

        // came back to chip
        if (transform.position == chipAttack.GetPosition())
        {
            chipAttack.CameBack();
        }
    }


    // going home
    public void GoHome()
    {
        am.PlaySFX(am.returnBoomerang);
        returning = true;
    }

}
