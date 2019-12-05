using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour
{
    private GameObject Player;
    private PhysicsPlayer PP;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PP = Player.GetComponent<PhysicsPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            PP.maxSpeed = 3;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PP.maxSpeed = 7;
        }
    }
}
