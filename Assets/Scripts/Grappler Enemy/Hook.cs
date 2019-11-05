using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        Debug.Log("Kill me");
        transform.position
    }
}
