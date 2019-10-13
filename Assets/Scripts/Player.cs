using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    private bool facingRight;

    internal void SetSpearInHand()
    {
        throw new NotImplementedException();
    }


    /* Spear related fields */

    public GameObject prefSpear;

    public GameObject spear;

    private GameObject Spear;

    Spear SpearScript;
    private bool SpearInHand = true;
    private bool SpawnOrRecallSpear;

    /// <summary>
    /// Set to true when the character intersects a collider beneath
    /// them in the previous frame.
    /// </summary>
    private bool grounded;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        prefSpear = Resources.Load("Spear") as GameObject;
    }

    private void Update()
    {
        // Use GetAxisRaw to ensure our input is either 0, 1 or -1.
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (grounded)
        {
            Debug.Log("gotem");
            velocity.y = 0;

            if (Input.GetButtonDown("Jump"))
            {
                // Calculate the velocity required to achieve the target jump height.
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }

        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
            Debug.Log("Right");
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }

        if (!grounded)
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime;
        } 

        transform.Translate(velocity * Time.deltaTime);



        grounded = false;

        // Retrieve all colliders we have intersected after velocity has been applied.
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            // Ignore our own collider.
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            // Ensure that we are still overlapping this collider.
            // The overlap may no longer exist due to another intersected collider
            // pushing us out of this one.
            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                // If we intersect an object beneath us, set grounded to true. 
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}