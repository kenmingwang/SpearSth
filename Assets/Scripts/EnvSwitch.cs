using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSwitch : MonoBehaviour
{
    [SerializeField]
    public GameObject doorAttached;

    private AudioSource audioSource;

    public AudioClip audioOpen;

    public AudioClip audioClose;

    private bool doorDown = true;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SwitchPressed()
    {
        if (doorDown)
        {
            //doorAttached = GameObject.Find("Door");
            Debug.Log("Hit button");
            audioSource.PlayOneShot(audioOpen);
            doorAttached.transform.Translate(Vector2.up * 100 * Time.deltaTime);
            doorDown = false;
        }
    }

    public void SwitchReleased()
    {
        if (!doorDown)
        {
            doorAttached.transform.Translate(Vector2.down * 100 * Time.deltaTime);
            audioSource.PlayOneShot(audioClose);
            doorDown = true;
        }
    }
}
