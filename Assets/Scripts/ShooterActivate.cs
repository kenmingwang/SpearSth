using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterActivate : MonoBehaviour
{
    private GameObject shooter;
    // Start is called before the first frame update
    void Start()
    {
        shooter = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            shooter.GetComponent<EnemyShooter>().ActivateShooter();
    }
}
