using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    Rigidbody2D rb2d;
    public LayerMask groundMask;
    private float walkSpeed = 8f;
    public bool canJump = true;
    public float jumpValue = 0.0f;
    private BoxCollider2D boxCollider2d;
    public PhysicsMaterial2D bounceMat, normalMat;
    bool facingRight = true;
    public AudioClip jump;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {

        // Jumps not working on FixedUpdate

        if (CrossPlatformInputManager.GetButtonDown("Jump") && IsGrounded() && canJump)
        {
            rb2d.velocity = new Vector2(0.0f, rb2d.velocity.y);
        }

        if (CrossPlatformInputManager.GetButtonUp("Jump"))
        {
            if (facingRight && IsGrounded())
            {
                float tempx = 8f;
                float tempy = jumpValue;
                rb2d.velocity = new Vector2(tempx, tempy);
                audioSource.PlayOneShot(jump, 0.3f);
                Invoke("ResetJump", 0.05f);
            }
            else if (!facingRight && IsGrounded())
            {
                float tempx = -8f;
                float tempy = jumpValue;
                rb2d.velocity = new Vector2(tempx, tempy);
                audioSource.PlayOneShot(jump, 0.3f);
                Invoke("ResetJump", 0.05f);
            }
            canJump = true;
        }
    }

    private void FixedUpdate()
    {
        // Movements and jumps

        horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        if(horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if(horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        if (jumpValue == 0.0f && IsGrounded())
        {
            rb2d.velocity = new Vector2(horizontalInput * walkSpeed, rb2d.velocity.y);
        }

        if (CrossPlatformInputManager.GetButton("Jump") && IsGrounded() && canJump)
        {
            jumpValue += 0.6f;
        }
        if (jumpValue >= 32f && !facingRight && IsGrounded())
        {
            float tempx = horizontalInput -8f;
            float tempy = jumpValue;
            rb2d.velocity = new Vector2(tempx, tempy);
            audioSource.PlayOneShot(jump, 0.3f);
            Invoke("ResetJump", 0.01f);
        }

        else if(jumpValue >= 32f && facingRight && IsGrounded())
        {
            float tempx = 8f;
            float tempy = jumpValue;
            rb2d.velocity = new Vector2(tempx, tempy);
            audioSource.PlayOneShot(jump, 0.3f);
            Invoke("ResetJump", 0.01f);
        }

        
    }

    void ResetJump()
    {
        canJump = false;
        jumpValue = 0;
    }
    private bool IsGrounded()
    {
        //Raycast

        float extraHeightText = 0.5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down,extraHeightText, groundMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
            canJump = true;
        } else {
            rayColor = Color.red;
            canJump = false;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x + extraHeightText/1.5f), rayColor);
        Debug.Log(raycastHit.collider);
        Debug.Log(raycastHit.collider);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;

    }

    void Flip()
    {
        //Player faces that direction

        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
}

