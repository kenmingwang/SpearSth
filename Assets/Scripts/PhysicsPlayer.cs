using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Ok so all of this code (besides spear spawning is stolen from https://learn.unity.com/tutorial/live-session-2d-platformer-character-controller
 * It is a tutorial so I feel less bad, but it is good to know that this is all stolen
 */
public class PhysicsPlayer : PhysicsObject
{
    //creates a public range for the more sensitive variables
    [Range(1, 50)]
    public float maxSpeed = 7;

    //creates a public range for the more sensitive variables
    [Range(1, 50)]
    public float jumpTakeOffSpeed = 7;

    //creates a public range for the more sensitive variables
    [Range(1.0f, 0.0f)]
    public float jumpDecay;

    [Tooltip("Sets the modifier for moving in midair")]
    [Range(0.0f, 1.0f)]
    public float airMovementMod;

    public LayerMask layerMask;

    public AudioClip throwAudio;

    AudioSource audioSource;

    /* Spear related fields */

    private GameObject prefSpear;
    private GameObject Spear;

    Spear SpearScript;
    private bool SpearInHand = true;
    private bool SpawnOrRecallSpear;
    private bool PlayerAlive = true;


    private SpriteRenderer spriteRenderer;
    //private Animator animator;

    // Use this for initialization
    void Awake()
    {
        prefSpear = Resources.Load("Spear") as GameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        // animator = GetComponent<Animator>();
    }

    internal void SetSpearInHand()
    {
        SpearInHand = !SpearInHand;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if (grounded)
        {
            move.x = Input.GetAxis("Horizontal");
        } else
        {
            move.x = 0.6f * Input.GetAxis("Horizontal");
        }
        

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * jumpDecay;
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
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("fire");

            if (SpearInHand)
            {
                Quaternion angle;
                Vector3 spwanPos;
                float x;
                // face left
                if (spriteRenderer.flipX)
                {
                    if (CheckCloseToWall(false))
                        return;
                    x = transform.position.x - 1;
                    spwanPos = new Vector3(x, transform.position.y + 0.4f);
                    angle = Quaternion.Euler(0, 0, 180);
                }
                else
                {
                    if (CheckCloseToWall(true))
                        return;
                    x = transform.position.x + 1;
                    spwanPos = new Vector3(x, transform.position.y + 0.4f);
                    angle = Quaternion.Euler(0, 0, 0);

                }
                Spear = Instantiate(prefSpear, spwanPos, angle);
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

    protected override void CheckPlayerStatus()
    {

    }

    public void Die()
    {
        PlayerAlive = false;
    }

    public bool IsAlive()
    {
        return PlayerAlive;
    }

    private bool CheckCloseToWall(bool flipX)
    {
        Vector2 dir = flipX ? Vector2.right : Vector2.left;
        // Check if too close to wall
        RaycastHit2D Hit = Physics2D.Raycast(transform.position, dir, 2f, layerMask);
        Debug.DrawRay(transform.position, dir, Color.red);
        if (Hit)
        {
            Debug.Log(Hit.collider.tag);
            return true;
        }
        return false;
    }
}
