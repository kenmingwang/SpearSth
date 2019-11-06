using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSwitch : MonoBehaviour
{
    [SerializeField]
    public GameObject doorAttached;

    private bool doorDown = true;

    public void SwitchPressed()
    {
        if (doorDown)
        {
            doorAttached = GameObject.Find("Door");
            Debug.Log("Hit button");
            doorAttached.transform.Translate(Vector2.right * 100 * Time.deltaTime);
            doorDown = false;
        }
    }

    public void SwitchReleased()
    {
        if (!doorDown)
        {
            doorAttached.transform.Translate(Vector2.left * 100 * Time.deltaTime);
            doorDown = true;
        }
    }
}
