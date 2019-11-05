using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    private bool grappleDeployed = false;

    private GameObject hook;
    private GameObject prefHook;


    void Awake()
    {
        prefHook = Resources.Load("Hook") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!grappleDeployed)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y - 0.3f);

            //This one line here makes a red ray to act as a trip wire of sorts. Feel free to delete when not needed 
            //THIS IS DISPLAY ONLY. IT HAS NO EFFECT ON DECTECTION!!!!!
            Debug.DrawRay(pos, transform.TransformDirection(Vector2.down) * 10, Color.red);

            if (hit.collider != null && hit.collider.name == "Player")
            {
                Debug.Log("Spotted Player");
                StartCoroutine(GrabPlayer());
                grappleDeployed = true;
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
        hook = Instantiate(prefHook, spawnPos, Quaternion.Euler(0, 0, 0));
    }
}
