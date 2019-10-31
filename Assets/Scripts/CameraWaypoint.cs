using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWaypoint: MonoBehaviour
{
    public GameObject prevCameraPos;
    public GameObject nextCameraPos;


    private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("MegaCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("passed");
            if (camera.transform.position.x == prevCameraPos.transform.position.x && camera.transform.position.y == prevCameraPos.transform.position.y)
            {
                Debug.Log("dab");
                float newX = prevCameraPos.transform.position.x - nextCameraPos.transform.position.x;
                float newY = prevCameraPos.transform.position.y - nextCameraPos.transform.position.y;
                camera.transform.Translate(-newX, -newY, 0.0f);
            }
            else
            {
                Debug.Log("go back");
                float newX = prevCameraPos.transform.position.x - nextCameraPos.transform.position.x;
                float newY = prevCameraPos.transform.position.y - nextCameraPos.transform.position.y;
                camera.transform.Translate(newX, newY, 0.0f);
            }
            //Debug.Log(prevCameraPos.transform.position.x);
        }


    }
}
