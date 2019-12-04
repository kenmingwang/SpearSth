using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterEnemyBtn : MonoBehaviour
{
    public GameObject btn_one;
    public GameObject btn_two;
    public GameObject SpawnDoor;
    public GameObject Enemy;
    public Animator ani;
    public Animation anim;
    public Material hitShader;
    public Material normalShader;

    private Renderer rend_one;
    private Renderer rend_two;
    private int counter;
    private float countDown;
    private float spwanCountDown;
    private float castCounDown;
    private int spawnCounter;
    private bool hit = false;
    private bool casting;
    // Start is called before the first frame update
    void Start()
    {
        rend_one = btn_one.GetComponent<Renderer>();
        rend_two = btn_two.GetComponent<Renderer>();
        counter = 0;
        countDown = 5f;
        spwanCountDown = 1f;
        castCounDown = 3f;
        
    }

    // Update is called once per frame
    void Update()
    {
        Cast();
        if (casting)
        {
            SpawnEnemy();
        }

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
        countDown = 5f;
        hit = false;

    }

    private void Cast()
    {
        castCounDown -= Time.deltaTime;
        if(castCounDown < 0)
        {
            ani.SetTrigger("Cast");
            castCounDown = 10f;
            casting = true;
            spawnCounter = 0;
        }
    }

    private void SpawnEnemy()
    {
        spwanCountDown -= Time.deltaTime;
        if(spwanCountDown < 0)
        {
            spwanCountDown = 1f;
            Vector3 pos = new Vector3(SpawnDoor.transform.position.x, SpawnDoor.transform.position.y + 0.1f, SpawnDoor.transform.position.z);
            
            Instantiate(Enemy, pos, new Quaternion());
           
            if (++spawnCounter > 5)
            {
                casting = false;
            }
        }
    }
}
