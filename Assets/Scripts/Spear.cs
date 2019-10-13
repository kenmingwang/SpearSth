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
    private Vector3 staticPos;


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
            GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
        }
       
        else if (isRecallTriggered)
        {
            transform.LookAt(Player.transform);
            transform.Rotate(transform.rotation.x, 90, 90);
            GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Hit Wall");
            isInWall = true;
            isThrowTriggered = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player2D");
            Destroy(gameObject);
            Player.GetComponent<PhysicsPlayer>().SetSpearInHand();
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