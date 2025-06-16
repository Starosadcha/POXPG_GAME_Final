using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float runSpeed = 7;
    public float jumpForce = 300;
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    public GroundChecker groundChecker;
    public PlayerHealth health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (health.isDead) return;

        float moveInput = Input.GetAxis("Horizontal");

        // Flip character sprite
        if (moveInput >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Running animation
        anim.SetBool("IsRun", moveInput != 0);

        // Move
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            anim.SetBool("IsJumping", true);
        }

        // Ground check to reset jump animation
        anim.SetBool("IsJumping", !groundChecker.isGrounded);
    }
}