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
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public LayerMask layerMask;

    public LayerMask movingLayer;

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
    private GameObject hitMovingPlatform;
    //private Animator animator;

    // Use this for initialization
    void Awake()
    {
        prefSpear = Resources.Load("Spear") as GameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource = GetComponent<AudioSource>();
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
        if (CheckStandOnMovingPlatform())
        {

        }
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
        RaycastHit2D Hit = Physics2D.Raycast(transform.position, dir, 2.3f, layerMask);
        Debug.DrawRay(transform.position, dir, Color.red);
        if (Hit)
        {
            Debug.Log(Hit.collider.tag);
            return true;
        }
        return false;
    }

    private bool CheckStandOnMovingPlatform()
    {
        RaycastHit2D Hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, movingLayer);
        Debug.DrawRay(transform.position, Vector2.down, Color.red);
        if (Hit)
        {
            hitMovingPlatform = Hit.collider.gameObject;
            Debug.Log(Hit.collider.tag + "moving");
            Hit.collider.gameObject.GetComponent<MovingPlatform>().TriggerStanding();
            transform.parent = Hit.collider.gameObject.transform;
            return true;
        }
        else
        {
            if (hitMovingPlatform != null)
            {
                hitMovingPlatform.GetComponent<MovingPlatform>().TriggerExit();
                hitMovingPlatform = null;
            }
            transform.parent = null;
        }
        return false;
    }

    internal void DamagedAction()
    {
        System.Random r = new System.Random();

        int ran = r.Next(-1, 1);
        if(ran <= 0)
        {
            ran = -1;
        }
        else
        {
            ran = 1;
        }
        transform.position += new Vector3(ran * 0.3f, 0.2f, 0);
    }

    internal void flash(bool v)
    {
        spriteRenderer.enabled = v;
    }


}
