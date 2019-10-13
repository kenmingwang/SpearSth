using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ok so all of this code (besides spear spawning is stolen from https://learn.unity.com/tutorial/live-session-2d-platformer-character-controller
 * It is a tutorial so I feel less bad, but it is good to know that this is all stolen
 */
public class PhysicsPlayer : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    //private Animator animator;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       // animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

       bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f) );
       if (flipSprite && move.x != 0f)
       {
           spriteRenderer.flipX = !spriteRenderer.flipX;
       }

        // animator.SetBool("grounded", grounded);
        // animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}
