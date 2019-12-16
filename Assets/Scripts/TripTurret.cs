using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripTurret : MonoBehaviour
{
    private bool grappleDeployed = false;

    private GameObject hook;
    private GameObject prefHook;
    private GameObject bullet;

    public GameObject bulletPrefabTransform;

    public float aimingPeriod;
    private float aimingLeft;


    void Awake()
    {
        aimingLeft = aimingPeriod;

        GameObject myLine = new GameObject();
        myLine.transform.position = this.transform.position;
        Vector2 dest = this.transform.position;
        dest.y = this.transform.position.y - 10.0f;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!grappleDeployed)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y - 0.3f);

            if (hit.collider != null && hit.collider.name == "Player")
            {
                Debug.Log("Spotted Player");
                Shoot();
                //StartCoroutine(GrabPlayer());
                //grappleDeployed = true;
            }
        }
    }

    private void ReelIn(GameObject grabbedUnit)
    {
        if (!grabbedUnit)
        {
            Debug.Log("caught nothing");
        }
    }

    private IEnumerator GrabPlayer()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 0.4f);
        Debug.Log("Firing capture device");
        yield return new WaitForSeconds(1);
       // hook = Instantiate(prefHook, spawnPos, Quaternion.Euler(0, 0, 0));
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
        //Destroy(myLine, duration);
    }

    private void Shoot()
    {
        aimingLeft -= Time.deltaTime;

        if (aimingLeft < 0)
        {
            Debug.Log("Shoot");
            //shootingTip.transform.LookAt(player.transform);
            //var target = player.transform.position - transform.position;
            var spawnPos = this.transform.position;
            Debug.Log(this.transform.rotation);
            bullet = Instantiate(bulletPrefabTransform, spawnPos, this.transform.rotation);
            aimingLeft = aimingPeriod;
        }

    }
}
