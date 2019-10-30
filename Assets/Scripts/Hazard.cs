using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy hit player");
            if (!GameObject.Find("Player").GetComponent<HealthSystem>().IsWUDI())
            {
                GameObject.Find("Player").GetComponent<PhysicsPlayer>().DamagedAction();
            }
            GameObject.Find("Player").GetComponent<HealthSystem>().Damaged(collision.gameObject);

            
            // Player.GetComponent<PhysicsPlayer>().SetSpearInHand();
        }
    }
}
