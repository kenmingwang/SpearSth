using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    public float speed;

    private bool facingRight = true;

    public Transform groundDectecton;

    private RaycastHit2D wallInfoForeward;

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 10;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

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
            if(wallInfoForeward.collider.tag == "Player")
            {
                return;
            }

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
