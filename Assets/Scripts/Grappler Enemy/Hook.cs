using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [Range(1, 50)]
    public float hookSpeed;

    [Range(1, 50)]
    public float recallSpeed;

    //public GameObject

    private bool hasCatch = false;

    private Vector2 velocity;

    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    private void Awake()
    {
        Debug.Log("Device fired");
        
        velocity.y -= hookSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            velocity.y = 0;

            hasCatch = true;


            //private GameObject player;
        }
        Debug.Log("Attack ");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("attacked c");
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("attacked ground");
        }
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }
}
