using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float transportPeriod;
    private float timeLeft;
    public float aimingPeriod;
    private float aimingLeft;

    public GameObject player;
    public GameObject firstPlat;
    public GameObject secondPlat;
    public GameObject thirdPlat;
    public Vector3 offset;

    private GameObject[] platToMove;
    private GameObject shootingTip;
    private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        shootingTip = GameObject.Find("shootingTip");
        bullet = Resources.Load("Bullet") as GameObject;
        timeLeft = transportPeriod;
        aimingLeft = aimingPeriod;
        platToMove = new GameObject[3];
        platToMove[0] = firstPlat;
        platToMove[1] = secondPlat;
        platToMove[2] = thirdPlat;
    }

    // Update is called once per frame
    void Update()
    {
        TransportInPeriod();
        Aim();
        Shoot();
    }

    private void TransportInPeriod()
    {
        foreach (var p in platToMove)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Debug.Log("tp");
                transform.position = p.transform.position + offset;
                timeLeft = transportPeriod;
            }
        }
    }

    private void Aim()
    {
        
        Vector2 firePos = shootingTip.transform.position;
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 dir = firePos - playerPos;
        Debug.DrawRay(firePos,-dir*50 , Color.red);
    }

    private void Shoot()
    {
        aimingLeft -= Time.deltaTime;
        
        if (aimingLeft < 0)
        {
            Debug.Log("Shoot");
            var spawnPos = shootingTip.transform.localPosition;
            bullet = Instantiate(bullet, spawnPos, new Quaternion(0,90,0,0));
            bullet.transform.LookAt(player.transform);
            // bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(50,0));
            aimingLeft = aimingPeriod;
        }
        
    }

}
