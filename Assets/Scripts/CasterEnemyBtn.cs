using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterEnemyBtn : MonoBehaviour
{
    public GameObject btn_one;
    public GameObject btn_two;

    public Material hitShader;
    public Material normalShader;

    private Renderer rend_one;
    private Renderer rend_two;
    private int counter;
    private float countDown;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        rend_one = btn_one.GetComponent<Renderer>();
        rend_two = btn_two.GetComponent<Renderer>();
        counter = 0;
        countDown = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 2)
        {
            counter = 0;
            GetComponent<HealthSystem>().Damaged(gameObject);
            ResetBtn();
        }
        if (hit)
        {
            CountDownStart();
        }
    }
    public void BtnHit(GameObject btn)
    {
        if (btn == btn_one)
            rend_one.material = hitShader;
        else
            rend_two.material = hitShader;

        counter++;
        hit = true;
    }

    private void CountDownStart()
    {
        countDown -= Time.deltaTime;
        if(countDown < 0)
        {
            countDown = 5f;
            counter = 0;
            ResetBtn();
        }
    }

    private void ResetBtn()
    {
        rend_one.material = normalShader;
        rend_two.material = normalShader;
        hit = false;

    }
}
