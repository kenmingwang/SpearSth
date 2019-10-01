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

    [SerializeField, Tooltip("Gravity affecting the character")]
    float gravityValue = -2;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    public GameObject prefSpear;
    public GameObject spear;
    Spear SpearScript;

    private bool grounded;

    private bool SpearInHand = true;
    private bool SpawnOrRecallSpear;

    void Start()
    {
        prefSpear = Resources.Load("Spear") as GameObject;
    }
    private void FixedUpdate()
    {

        transform.Translate(velocity * Time.deltaTime);

        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }


        transform.rotation = new Quaternion(0, 0, 0,0);
        float horizontal = Input.GetAxis("Horizontal");


        if (grounded)
        {
            velocity.y = 0;

            // Jumping code we implemented earlier—no changes were made here.
            if (Input.GetButtonDown("Jump"))
            {
                // Calculate the velocity required to achieve the target jump height.
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }

        velocity.y += gravityValue * Time.deltaTime;

        grounded = false;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
        foreach (Collider2D hit in hits)
        {
            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (hit == boxCollider)
                continue;

           

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }

            if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
            {
                grounded = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (SpearInHand)
            {
                 
                float x = transform.position.x + 2;
                Vector3 spwanPos = new Vector3(x, transform.position.y, transform.position.z);
                spear = Instantiate(prefSpear, spwanPos, Quaternion.Euler(0, 0, -90));
                SpearScript = spear.GetComponent<Spear>();
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spear" && !SpawnOrRecallSpear)
        {
            Destroy(collision.gameObject);
            SpearInHand = true;
        }

        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    public void SetSpearInHand()
    {
        SpearInHand = true;
    }
}
