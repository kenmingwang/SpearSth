using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    Vector3 fwd;

    public GameObject Player;
    public int SpearSpeed=75;

    public AudioClip spearHit;
    
    public AudioClip throwAudio;
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

            GetComponent<Rigidbody2D>().AddForce(transform.right * 100);
        }

        else if (isRecallTriggered)
        {
            //Add turn off collision code here
             transform.LookAt(Player.transform);
            transform.Rotate(0, 90, -180);

            GetComponent<Rigidbody2D>().AddForce(transform.right * 100);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            wallPos = transform.position;
            wallRot = transform.rotation;
            Debug.Log("Hit Wall");
            isInWall = true;
            isThrowTriggered = false;

            SetIsTrigger();
        } else if (collision.gameObject.tag == "Switch")
        {
            wallPos = transform.position;
            wallRot = transform.rotation;
            Debug.Log("Hit Switch"); 
            collision.gameObject.GetComponent<EnvSwitch>().SwitchPressed();
            isInWall = true;
            isThrowTriggered = false;
            SetIsTrigger();
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player2D");
            Destroy(gameObject);
            Player.GetComponent<PhysicsPlayer>().SetSpearInHand();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Switch")
        {
            Debug.Log("Dank Release");
            collision.gameObject.GetComponent<EnvSwitch>().SwitchReleased();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyParent>().Damaged(collision.gameObject);

            audioSource.PlayOneShot(spearHit, 0.4f);
            Debug.Log("Hit Enemy");

        }
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
            
        }
        isRecallTriggered = true;
    }


}