using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float m_speed = 5.0f;
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
        transform.rotation = new Quaternion(0, 0, 0,0);
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * horizontal * m_speed * Time.deltaTime);// A D
        }

        // Jump
        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded == true)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(0, 7, 0);
                GetComponent<Rigidbody>().AddForce(Vector3.up * 50);
                grounded = false;
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
