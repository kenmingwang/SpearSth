using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyParent : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxHealth { get; set; }
    private float currentHealth { get; set; }

    private GameObject Canvas { get; set; }
    private GameObject hbarObj { get; set; }
    private RectTransform recTrans{get;set;}

    private Slider healthBar { get; set; }

    public Vector2 offset { get; set; }

    GameObject prefab;
    void Start()
    {    
        Init();
    }

    protected void Init()
    {
        Canvas = GameObject.Find("Canvas");
        // Instantiate slider(healthbar)
        prefab = Resources.Load("HealthBar") as GameObject;
        hbarObj = Instantiate(prefab);
        healthBar = hbarObj.GetComponent<Slider>();
        healthBar.transform.SetParent(Canvas.transform);
        recTrans = hbarObj.GetComponent<RectTransform>();

        HealthBarFollow();

        // Set max health
        maxHealth = 2;
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        CalculateHealth();

        //Differs every enemy
        offset = new Vector2(0, 50);

    }

    // Update is called once per frame
    void Update()
    {
        CalculateHealth();
        HealthBarFollow();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy hit player");
            Destroy(gameObject);
            // Player.GetComponent<PhysicsPlayer>().SetSpearInHand();
        }
    }

    public void Damaged(GameObject gameObj)
    {
        if(--currentHealth <= 0)
        {
            Die(gameObj);
        }
    }

    private void Die(GameObject gameObj)
    {
        Destroy(gameObj);
        Destroy(hbarObj);
    }

    private void CalculateHealth()
    {
        healthBar.value = currentHealth;
    }

    private void HealthBarFollow()
    {
        Vector3 tarPos = transform.position;
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, tarPos);
        recTrans.position = pos + offset;
    }
}
