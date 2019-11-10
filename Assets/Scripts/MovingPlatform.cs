using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Only moves when player is on it
    public bool moveOnTouch;
    private bool activateMove;
    // If it's elavator, then it moves vertical
    public bool vertical;

    [SerializeField]
    [Header("Expect only y values if vertical is checked")]
    public Vector3 moveVelocity;
    [SerializeField]
    [Header("Expect only y values if vertical is checked")]

    public Vector3 moveRange;

    private Vector3 endPoint1;
    private Vector3 endPoint2;
    private bool movingBack;

    private List<Rigidbody2D> rgb = new List<Rigidbody2D>();
    private Vector3 LastPos;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        LastPos = _transform.position;

        endPoint1 = transform.position + moveRange;
        endPoint2 = transform.position - moveRange;
    }

    // Update is called once per frame
    void Update()
    {
        activateMove = !moveOnTouch;
    }

    private void FixedUpdate()
    {
        if (activateMove)
        {
            if (!CheckRange() && !movingBack)
            {
                Move(false);
            }
            else
            {
                Move(true);
            };
        }
    }

    private void Move(bool revert)
    {
        if (!revert)
        {
            transform.position += (moveVelocity * Time.deltaTime);
        }
        else
        {
            transform.position -= (moveVelocity * Time.deltaTime);
        }

    }


    private bool CheckRange()
    {
        if (!vertical)
        {
            if (transform.position.x > endPoint1.x)
            {
                movingBack = true;
                return true;
            }
            else if (transform.position.x < endPoint2.x)
            {
                movingBack = false;
                return false;
            }
            return false;
        }
        else
        {
            if (transform.position.y > endPoint1.y)
            {
                movingBack = true;
                return true;
            }
            else if (transform.position.y < endPoint2.y)
            {
                movingBack = false;
                return false;
            }
            return false;
        }

    }

    private void LateUpdate()
    {
        if(rgb.Count > 0)
        {
            foreach(var rb in rgb)
            {
                Vector3 v = (_transform.position - LastPos);
                rb.transform.Translate(v, _transform);
            }
           
        }
        LastPos = _transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            activateMove = true;
            rgb.Add(collision.collider.GetComponent<Rigidbody2D>());
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Debug.Log("hit player");
            TriggerExit();
            rgb.RemoveAt(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("unhit player");

    //        activateMove = !moveOnTouch;
    //        collision.gameObject.transform.parent = null;
    //    }
    //}

    public void TriggerStanding()
    {
        activateMove = true;

    }

    public void TriggerExit()
    {
        activateMove = !moveOnTouch;

    }
}