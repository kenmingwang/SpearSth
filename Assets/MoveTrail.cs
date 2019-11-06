using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{

    public int moveSpeed = 5;
    public GameObject Player;

    private bool lookAt = false;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (!lookAt)
        {
            transform.Rotate(0, 90, -180);
            transform.GetComponent<Rigidbody2D>().velocity = (transform.right * 30);
            lookAt = true;
        }                   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthSystem>().Damaged(collision.gameObject);
            collision.gameObject.GetComponent<PhysicsPlayer>().DamagedAction();
            Destroy(gameObject);
        }
    }


}
