using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSwitch : MonoBehaviour
{
    public GameObject doorAttached;

    private float DoorMovement = 2.0f;

    private bool doorDown = true;

    public void SwitchPressed()
    {
        if (doorDown)
        {
            //doorAttached = GameObject.Find("Door");
            Debug.Log("Hit button");
            doorAttached.transform.Translate(DoorMovement, 0.0f, 0.0f);
            doorDown = false;
        }
    }

    public void SwitchReleased()
    {
        if (!doorDown)
        {
            doorAttached.transform.Translate(-DoorMovement, 0.0f, 0.0f);
            doorDown = true;
        }
    }
}
