using System;
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

    /* Spear related fields */

    private GameObject prefSpear;
    private GameObject Spear;

    Spear SpearScript;
    private bool SpearInHand = true;
    private bool SpawnOrRecallSpear;


    private SpriteRenderer spriteRenderer;
    //private Animator animator;

    // Use this for initialization
    void Awake()
    {
        prefSpear = Resources.Load("Spear") as GameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // animator = GetComponent<Animator>();
    }

    internal void SetSpearInHand()
    {
        SpearInHand = !SpearInHand;
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

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.0001f) : (move.x < 0.0001f));
        if (flipSprite && move.x != 0f)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        // animator.SetBool("grounded", grounded);
        // animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    protected override void CheckSpearStatus()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("fire");

            if (SpearInHand)
            {
                float x = transform.position.x + 2;
                Vector3 spwanPos = new Vector3(x, transform.position.y);
                Spear = Instantiate(prefSpear, spwanPos, Quaternion.Euler(0, 0, -90));
                SpearScript = Spear.GetComponent<Spear>();
                SpearScript.TriggerThrow();
                SpawnOrRecallSpear = true;
                SpearInHand = false;
            }
            else if (!SpearInHand && SpearScript.GetWallStatus())
            {
                SpearScript.TriggerRecall();
                SpawnOrRecallSpear = false;
            }


        }
    }


}
