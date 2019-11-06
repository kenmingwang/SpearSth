using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [Range(1, 50)]
    public float hookSpeed;

    [Range(1, 50)]
    public float recallSpeed;

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
        Debug.Log("attacked");
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
