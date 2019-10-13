using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    Vector3 fwd;

    public GameObject Player;
    private bool isInWall = false;
    private bool isThrowTriggered = false;
    private bool isRecallTriggered = false;


    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        Vector2 pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrowTriggered)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * 100);
        }

        if (transform.position.x < Player.transform.position.x - 10)
        {
            Destroy(gameObject);
            Player.GetComponent<PhysicsPlayer>().SetSpearInHand();
        }
       
        else if (isRecallTriggered)
        {
            transform.LookAt(Player.transform);
            transform.Rotate(transform.rotation.x, 90, 90);
            GetComponent<Rigidbody>().AddForce(transform.up * 100);
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Hit Wall");
            isInWall = true;
            isThrowTriggered = false;
        }
    }



    public bool GetWallStatus()
    {
        return isInWall;
    }

    public void TriggerThrow()
    {
        isThrowTriggered = true;
    }

    public void TriggerRecall()
    {
        if (isInWall == true)
        {
            isInWall = false;
        }
        isRecallTriggered = true;
    }


}