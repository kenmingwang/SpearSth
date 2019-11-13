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

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfoDown = Physics2D.Raycast(groundDectecton.position, Vector2.down, 0.5f);

        if (facingRight)
        {
            wallInfoForeward = Physics2D.Raycast(groundDectecton.position, Vector2.right, 0.01f);
        } else
        {
            wallInfoForeward = Physics2D.Raycast(groundDectecton.position, Vector2.left, 0.01f);
        }
        if (!groundInfoDown.collider || wallInfoForeward.collider)
        {
            if (facingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingRight = false;

            } else
            {
                transform.eulerAngles = new Vector3(0, -0, 0);
                facingRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
