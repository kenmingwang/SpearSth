using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    public float speed;

    private bool facingRight = true;

    public Transform groundDectecton;

    private RaycastHit2D wallInfoForeward;

    public Animator animator;

    private bool isAlive = true;
    private bool dying = false;
    private bool Activate = false;


    float distance = 0.7f;
    float beforeTrans;

    private void Start()
    {
        beforeTrans = transform.position.y;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {


            transform.Translate(Vector2.right * speed * Time.deltaTime);

            RaycastHit2D groundInfoDown = Physics2D.Raycast(groundDectecton.position, Vector2.down, 0.5f);

            if (facingRight)
            {
                wallInfoForeward = Physics2D.Raycast(groundDectecton.position, Vector2.right, 0.01f);
            }
            else
            {
                wallInfoForeward = Physics2D.Raycast(groundDectecton.position, Vector2.left, 0.01f);
            }

            if (!groundInfoDown.collider)
            {
                Turn();
            }
            else if (wallInfoForeward.collider)
            {
                if (wallInfoForeward.collider.tag == "Player")
                {
                    return;
                }

                Turn();
            }
        }
        else if (dying)
        {
            if (beforeTrans - transform.position.y < 0.7)
                transform.position -= new Vector3(0, 0.9f, 0) * Time.deltaTime;
        }
    }

    private void Turn()
    {
        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = false;

        }
        else
        {
            transform.eulerAngles = new Vector3(0, -0, 0);
            facingRight = true;
        }
    }

    public void Die()
    {
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.3f, 1.6f);
        GetComponent<BoxCollider2D>().size = new Vector2(12f, 4f);
        animator.SetTrigger("dying");
        isAlive = false;
        dying = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isAlive)
        {
            animator.SetTrigger("attacking");
            Debug.Log("Enemy hit player");
            var player = GameObject.Find("Player");
            if (!player.GetComponent<HealthSystem>().IsWUDI())
            {
                player.GetComponent<PhysicsPlayer>().DamagedAction();
            }
            player.GetComponent<HealthSystem>().Damaged(player);
            // Player.GetComponent<PhysicsPlayer>().SetSpearInHand();
        }
    }
}