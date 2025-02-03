using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// we have a separate chipAttack object because 
// chip is the only one with a boomerang
// and we don't want any confusion 
public class ChipAttack : MonoBehaviour
{
    // varibles for playercontroller and boomerang
    private PlayerController playerController;
    private GameObject boomerang;
    private Boomerang boomerangScript;

    // does chip have her boomerang?
    private bool hasBoomerang;

  
    private AudioManager am;


    // Start is called before the first frame update
    void Start()
    {
        // find the player controller and boomerang
        playerController = GetComponent<PlayerController>();
        boomerang = GameObject.Find("Boomerang");
        boomerang.SetActive(false);

        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // she always has her boomerang to start
        hasBoomerang = true;
    }

    // Update is called once per frame
    void Update()
    {
        // throw the boomerang if it isn't already deployed
        if (Input.GetKeyDown(KeyCode.LeftShift) && hasBoomerang)
            Attack();
    }

    // play the sound, set boomerang to active
    public void Attack()
    {
        am.PlaySFX(am.departBoomerang);
        boomerang.SetActive(true);
        hasBoomerang = false;
    }

    // boomerang is back
    public void CameBack()
    {
        boomerang.SetActive(false);
        hasBoomerang = true;
    }

    // return chip's position
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // tell the boomerang to come home
    public void GoHome()
    {
        boomerangScript.GoHome();
    }

    // does chip have her boomerang?
    // this lets us find out from other scripts
    public bool HasBoomerang()
    {
        return hasBoomerang;
    }
}
