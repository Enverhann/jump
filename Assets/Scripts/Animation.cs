using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Animation : MonoBehaviour
{
    private Animator anim;
    private PlayerController playerControllerScript;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        // All animation boools and triggers

        if(playerControllerScript.horizontalInput<0 || playerControllerScript.horizontalInput > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if(playerControllerScript.canJump == false)
        {
            anim.SetTrigger("jump");
        }
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            anim.SetBool("isWalking", false);
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            anim.SetBool("isWalking", false);
        }
    }
}
