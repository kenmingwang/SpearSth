using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    public float hookSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = transform.position;
    }

    private void Awake()
    {
        Debug.Log("Kill me");
        //transform.position();
    }
}
