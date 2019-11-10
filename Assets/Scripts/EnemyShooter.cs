using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Time frames")]
    public float transportPeriod;
    private float timeLeft;
    public float aimingPeriod;
    private float aimingLeft;

    private GameObject player;
    [Header("Order does not matter")]
    public GameObject firstPlat;
    public GameObject secondPlat;
    public GameObject thirdPlat;

    [Header("Add some Y value so it stands on top")]
    public Vector3 offset;
    public GameObject bulletPreafabTransform;

    // Private vars
    private GameObject[] platToMove;
    private GameObject shootingTip;
    private GameObject bullet;
    private Vector2 prevPlayerTran;
    private float playerPrevTime = 0.1f;
    
    private bool Activate = false;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player");
        prevPlayerTran = new Vector2(player.transform.position.x, player.transform.position.y);
    }
    void Start()
    {
        shootingTip = GameObject.Find("shootingTip");
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
        if (Activate)
        {
            TransportInPeriod();
            Aim(GetPrePlayerTran());
            Shoot();
        }
    }

    private Vector2 GetPrePlayerTran()
    {
        playerPrevTime -= Time.deltaTime;
        if (playerPrevTime < 0)
        {
            var tmp = prevPlayerTran;
            prevPlayerTran = new Vector2(player.transform.position.x, player.transform.position.y);
            playerPrevTime = 0.1f;
            return tmp;
        }
        else
        {
            return prevPlayerTran;
        }

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

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.05f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = Color.red;
        lr.endColor = Color.cyan;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.005f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Destroy(myLine, duration);
    }

    private void Aim(Vector2 pt)
    {
        Vector2 firePos = shootingTip.transform.position;
        Vector2 playerPos = new Vector2(pt.x, pt.y);
        Vector2 dir = firePos - playerPos;
        DrawLine(firePos, playerPos, Color.red);
    }

    private void Shoot()
    {
        aimingLeft -= Time.deltaTime;

        if (aimingLeft < 0)
        {
            Debug.Log("Shoot");
            shootingTip.transform.LookAt(player.transform);
            var target = player.transform.position - transform.position;
            var spawnPos = shootingTip.transform.position;
            Debug.Log(shootingTip.transform.rotation);
            bullet = Instantiate(bulletPreafabTransform, spawnPos, shootingTip.transform.rotation);
            aimingLeft = aimingPeriod;
        }

    }

    public void ActivateShooter()
    {
        Activate = true;
    }

}
