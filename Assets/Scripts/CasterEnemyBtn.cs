using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterEnemyBtn : MonoBehaviour
{
    public GameObject btn_one;
    private bool btn_one_hit;
    public GameObject btn_two;
    private bool btn_two_hit;
    public GameObject SpawnDoor;
    public GameObject Enemy;
    public Animator ani;
    public Animation anim;
    public Material hitShader;
    public Material normalShader;
    public Sprite BtnClosed;
    private Sprite BtnOpen;

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
        countDown = 10f;
        spwanCountDown = 2f;
        castCounDown = 3f;
        BtnOpen = btn_one.GetComponent<SpriteRenderer>().sprite;

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
        if (btn == btn_one && !btn_one_hit)
        {
            btn_one_hit = true;
            btn_one.GetComponent<SpriteRenderer>().sprite = BtnClosed;
            hit = true;
            counter++;
        }
        else if (btn == btn_two && !btn_two_hit)
        {
            btn_two_hit = true;
            btn_two.GetComponent<SpriteRenderer>().sprite = BtnClosed;
            hit = true;
            counter++;
        }
    }

    private void CountDownStart()
    {
        countDown -= Time.deltaTime;
        if (countDown < 0)
        {
            countDown = 10f;
            counter = 0;
            ResetBtn();
        }
    }

    private void ResetBtn()
    {
        btn_one.GetComponent<SpriteRenderer>().sprite = BtnOpen;
        btn_two.GetComponent<SpriteRenderer>().sprite = BtnOpen;
        btn_one_hit = false;
        btn_two_hit = false;
        countDown = 5f;
        hit = false;

    }

    private void Cast()
    {
        castCounDown -= Time.deltaTime;
        if (castCounDown < 0)
        {
            ani.SetTrigger("Cast");
            castCounDown = 13f;
            casting = true;
            spawnCounter = 0;
        }
    }

    private void SpawnEnemy()
    {
        spwanCountDown -= Time.deltaTime;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length > 4)
        {
            return;
        }
        if (spwanCountDown < 0)
        {
            spwanCountDown = 2f;
            Vector3 pos = new Vector3(SpawnDoor.transform.position.x, SpawnDoor.transform.position.y + 0.1f, SpawnDoor.transform.position.z);

            Instantiate(Enemy, pos, new Quaternion());

            if (++spawnCounter > 5)
            {
                casting = false;
            }
        }
    }

    public void DecrementAliveEnemey()
    {
        spawnCounter--;
    }
}