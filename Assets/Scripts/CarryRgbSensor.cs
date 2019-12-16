using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryRgbSensor : MonoBehaviour
{

    [HideInInspector] public MovingPlatform carrier;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if(rb != null && rb != carrier.r2d)
        {
            carrier.Add(rb);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null && rb != carrier.r2d)
        {
            carrier.Remove(rb);
        }
    }
}
