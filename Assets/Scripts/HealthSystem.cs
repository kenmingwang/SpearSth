using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth;
    public bool isPlayer;
    private float currentHealth { get; set; }

    private GameObject Canvas { get; set; }
    private GameObject hbarObj { get; set; }
    private RectTransform recTrans { get; set; }

    private Slider healthBar { get; set; }

    private bool invincible;
    private float invincibleTime;
    private PhysicsPlayer player;

    //controlls the audio of damage of the thing attached
    AudioSource audioSource;

    public AudioClip DmgSound;

    public AudioClip DeathSound;


    public Vector2 offset;

    GameObject prefab;
    void Start()
    {
        Init();
    }

    protected void Init()
    {

        if (isPlayer)
        {
            player = GameObject.Find("Player").GetComponent<PhysicsPlayer>();
        }
        Canvas = GameObject.Find("Canvas");

        audioSource = GetComponent<AudioSource>();

        // Instantiate slider(healthbar)
        prefab = Resources.Load("HealthBar") as GameObject;
        hbarObj = Instantiate(prefab);
        healthBar = hbarObj.GetComponent<Slider>();
        healthBar.transform.SetParent(Canvas.transform);
        recTrans = hbarObj.GetComponent<RectTransform>();
        invincible = false;
        invincibleTime = 3f;
        HealthBarFollow();

        // Set max health
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        CalculateHealth();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateWUDI();
        CalculateHealth();
        HealthBarFollow();
    }



    public void Damaged(GameObject gameObj)
    {
        if (!invincible)
        {      
            if (isPlayer)
            {
                WUDI();
            }
            if (--currentHealth <= 0)
            {
                if (!isPlayer)
                {
                    audioSource.PlayOneShot(DeathSound, 1f);
                }
                Die(gameObj);
            } else if (!isPlayer)
            {
                audioSource.PlayOneShot(DmgSound, 1f);
            }
        }
    }

    private void Die(GameObject gameObj)
    {
        var name = gameObj.name;
        Debug.Log(name + " died");
       // gameObj.SetActive(false);
        if (name == "ShooterEnemy")
        {
            Debug.Log("next");

            GameObject.Find("MegaCamera").GetComponent<GameManager>().NextStage();
        }
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


    // Player invincible for 2 seconds after being damaged
    private void WUDI()
    {
        invincible = true;
    }

    private void UpdateWUDI()
    {
        if (invincible)
        {
            invincibleTime -= Time.deltaTime;
            if (invincibleTime <= 0)
            {

                invincible = false;
                invincibleTime = 3f;
            }
            else
            {
                float remain = (3 - invincibleTime) % 0.3f;
                player.flash(remain > 0.15f);
            }
        }
    }

    public bool IsWUDI()
    {
        return invincible;
    }
}