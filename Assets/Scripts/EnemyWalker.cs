using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    public float speed;

    private bool facingRight = true;

    public Transform groundDectecton;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDectecton.position, Vector2.down, 1f);

        if(groundInfo.collider == false)
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

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy hit player");
            Destroy(gameObject);
           // Player.GetComponent<PhysicsPlayer>().SetSpearInHand();
        }     
    }
}
