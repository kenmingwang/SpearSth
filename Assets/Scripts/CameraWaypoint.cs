using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWaypoint : MonoBehaviour
{
    public GameObject prevCameraPos;
    public GameObject nextCameraPos;

    private GameObject player;
    public bool StartingWaypoint;
    public bool VerticalWaypoint;
    Renderer m_Renderer;

    private bool nearPrev;
    private bool nearNext;

    private float newX;
    private float newY;

    private GameObject camera;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("MegaCamera");

        m_Renderer = player.GetComponent<Renderer>();

        if (StartingWaypoint)
        {
            float newX = camera.transform.position.x - prevCameraPos.transform.position.x;
            float newY = camera.transform.position.y - prevCameraPos.transform.position.y;
            camera.transform.Translate(-newX, -newY, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCameraLocation();

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("passed");
            newX = prevCameraPos.transform.position.x - nextCameraPos.transform.position.x;
            newY = prevCameraPos.transform.position.y - nextCameraPos.transform.position.y;
            if (nearPrev)
            {
                Debug.Log("go foreward");
                camera.transform.Translate(-newX, -newY, 0.0f);
                camera.GetComponent<GameManager>().curCameraPos = nextCameraPos;
            }
            else if (nearNext)
            {
                Debug.Log("go back");
                camera.transform.Translate(newX, newY, 0.0f);
                camera.GetComponent<GameManager>().curCameraPos = prevCameraPos;
            }
            //Debug.Log(prevCameraPos.transform.position.x);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
         * This is to counter a bug where the player could stand withing the waypoint and turn around, confusing the code
         * To counter the bug, this code runs the camera movement check again and then runs the position of the player relative to the checkpoint
         * So when the player leaves the checkpoint, the code checks the camera position and the leave direction
         * If they are different switch the camera position back
         */
        if (collision.gameObject.tag == "Player")
        {
            CheckCameraLocation();
            bool playerNearPrev;

            if (!VerticalWaypoint)
            {
                playerNearPrev = player.transform.position.x < this.transform.position.x;
            } else
            {
                playerNearPrev = player.transform.position.y < this.transform.position.y;
            }

            Debug.Log("playerNearPrev: " + playerNearPrev);

            Debug.Log("nearNext: " + nearNext + " playerNearPrev: " + playerNearPrev);

            Debug.Log("nearPrev: " + nearPrev + " playerNearPrev: " + playerNearPrev);

            if (nearNext && playerNearPrev)
            {
                camera.transform.Translate(newX, newY, 0.0f);
                camera.GetComponent<GameManager>().curCameraPos = nextCameraPos;
            }
            else if (nearPrev && !playerNearPrev)
            {
                camera.transform.Translate(-newX, -newY, 0.0f);
                camera.GetComponent<GameManager>().curCameraPos = prevCameraPos;
            }
        }
    }

    private void CheckCameraLocation()
    {
        //These are some pretty beefy if statments. The movement of the camera is not perfect, so the if statements give a little wiggle room for camera if it misses slightly
        nearPrev = (camera.transform.position.x <= prevCameraPos.transform.position.x + 0.1f && camera.transform.position.x >= prevCameraPos.transform.position.x - 0.1f)
                && camera.transform.position.y <= prevCameraPos.transform.position.y + 0.1f && camera.transform.position.y >= prevCameraPos.transform.position.y - 0.1f;

        nearNext = (camera.transform.position.x <= nextCameraPos.transform.position.x + 0.1f && camera.transform.position.x >= nextCameraPos.transform.position.x - 0.1f)
                && camera.transform.position.y <= nextCameraPos.transform.position.y + 0.1f && camera.transform.position.y >= nextCameraPos.transform.position.y - 0.1f;
    }
}