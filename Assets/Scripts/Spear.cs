using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    Vector3 fwd;

    public GameObject Player;
    public float SpearSpeed=30f;

    public AudioClip spearHitEnemy;
    
    public AudioClip throwAudio;

    public AudioClip spearHitGround;
    AudioSource audioSource;

    private Vector3 wallPos;
    private Quaternion wallRot;
    private bool isInWall = false;
    private bool isThrowTriggered = false;
    private bool isRecallTriggered = false;
    private Vector3 staticPos;

    private void Awake()
    {
        Player = GameObject.Find("Player");

        audioSource = GetComponent<AudioSource>();

    }
    // Use this for initialization
    void Start()
    {
        Vector2 pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (isInWall)
        {
            transform.position = wallPos;
            transform.rotation = wallRot;
        }

    }

    private void FixedUpdate()
    {
        if (isThrowTriggered)
        {

            GetComponent<Rigidbody2D>().velocity = (transform.right * SpearSpeed);
        }

        else if (isRecallTriggered)
        {
            //Add turn off collision code here
             transform.LookAt(Player.transform);
            transform.Rotate(0, 90, -180);

            GetComponent<Rigidbody2D>().velocity = (transform.right * SpearSpeed);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            audioSource.PlayOneShot(spearHitGround, 0.08f);
            wallPos = transform.position;
            wallRot = transform.rotation;
            Debug.Log("Hit Wall");
            isInWall = true;
            isThrowTriggered = false;
            GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
            GetComponent<BoxCollider2D>().size = new Vector2(2.6f, 0.1f);
        } 
        else if (collision.gameObject.tag == "Switch")
        {
            wallPos = transform.position;
            wallRot = transform.rotation;
            Debug.Log("Hit Switch"); 
            collision.gameObject.GetComponent<EnvSwitch>().SwitchPressed();
            isInWall = true;
            isThrowTriggered = false;
        }
        else if (collision.gameObject.tag == "MovingPlat")
        {
            wallPos = transform.position;
            wallRot = transform.rotation;
            Debug.Log("Hit Wall");
            isInWall = true;
            isThrowTriggered = false;
            GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
            GetComponent<BoxCollider2D>().size = new Vector2(1.3f, 0.1f);
          // transform.SetParent(collision.gameObject.transform);
        }

    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Switch")
        {
            //Debug.Log("Dank Release");
            collision.gameObject.GetComponent<EnvSwitch>().SwitchReleased();

        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthSystem>().Damaged(collision.gameObject);
            Debug.Log("Hit Enemy");

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player2D");
            Destroy(gameObject);
            Player.GetComponent<PhysicsPlayer>().SetSpearInHand();
        }
    }


    private void SetIsTrigger()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<BoxCollider2D>().offset = new Vector2(1f, 0f);
        GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.2f);
    }


    public bool GetWallStatus()
    {
        return isInWall;
    }

    public void TriggerThrow()
    {
        audioSource.PlayOneShot(throwAudio, 0.4f);

        isThrowTriggered = true;
    }

    public void TriggerRecall()
    {
        if (isInWall == true)
        {
            isInWall = false;
            SetIsTrigger();
        }
        isRecallTriggered = true;
    }


}