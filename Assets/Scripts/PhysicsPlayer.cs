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
    private bool PlayerOnMovingPlat = false;

    private SpriteRenderer spriteRenderer;
    private GameObject hitMovingPlatform;
    public Animator animator;

    // Use this for initialization
    void Awake()
    {
        prefSpear = Resources.Load("Spear") as GameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource = GetComponent<AudioSource>();
        // animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAlive())
        {
            ComputeVelocity();
            CheckPlayerStatus();
            CheckSpearStatus();
        }
        else
        {
            targetVelocity = Vector2.zero;
        }
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
            //Debug.Log("move");
            move.x = Input.GetAxis("Horizontal");
            animator.SetFloat("running", Mathf.Abs(move.x));
            animator.SetBool("jumping", false);

        }
        else
        {
            move.x = airMovementMod * Input.GetAxis("Horizontal");
            // animator.SetFloat("jumping", Mathf.Abs(move.x));

        }


        if (Input.GetButtonDown("Jump") && (grounded || PlayerOnMovingPlat))
        {
            Debug.Log("jump");
            velocity.y = jumpTakeOffSpeed;
            animator.SetBool("jumping", true);

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
                animator.SetTrigger("throwing");
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
        else
        {
            //    animator.SetBool("throwing", false);
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
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.3f, -0.03f);
        GetComponent<BoxCollider2D>().size = new Vector2(12f, 4f);
        animator.SetTrigger("dying");
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
        //RaycastHit2D Hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, movingLayer);
        //Debug.DrawRay(transform.position, Vector2.down, Color.red);
        //if (Hit)
        //{
        //    PlayerOnMovingPlat = true;
        //    hitMovingPlatform = Hit.collider.gameObject;
        //    Debug.Log(Hit.collider.tag + "moving");
        //    Hit.collider.gameObject.GetComponent<MovingPlatform>().TriggerStanding();
        //    transform.parent = Hit.collider.gameObject.transform;
        //    return true;
        //}
        //else
        //{
        //    PlayerOnMovingPlat = false;
        //    if (hitMovingPlatform != null)
        //    {
        //        hitMovingPlatform.GetComponent<MovingPlatform>().TriggerExit();
        //        hitMovingPlatform = null;
        //    }
        //    transform.parent = null;
        //}
        return false;
    }

    internal void DamagedAction()
    {
        var ran = UnityEngine.Random.Range(-1f, 1f);
        if (ran <= 0)
        {
            ran = -1;
        }
        else
        {
            ran = 1;
        }
        base.Movement(new Vector2(ran * 1, 1), true);
    }

    internal void flash(bool v)
    {
        spriteRenderer.enabled = v;
    }


}
