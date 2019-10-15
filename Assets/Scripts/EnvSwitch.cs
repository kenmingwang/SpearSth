using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSwitch : MonoBehaviour
{
    [SerializeField]
    public GameObject doorAttached;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPressed()
    {
        doorAttached = GameObject.Find("Door");
        Debug.Log("Hit button");
        doorAttached.transform.Translate(Vector2.right * 100 * Time.deltaTime);
    }

    public void SwitchReleased()
    {
        doorAttached.transform.Translate(Vector2.left * 100 * Time.deltaTime);
    }
}
