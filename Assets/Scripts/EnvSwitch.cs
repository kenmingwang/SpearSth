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

    public Sprite doorOpen;

    public Sprite doorClosed;

    public Sprite buttonOpen;

    public Sprite buttonClosed;

    private bool doorDown = true;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void SwitchPressed()
    {
        if (doorDown)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = buttonOpen;
            doorAttached.gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
            audioSource.PlayOneShot(audioOpen);
            doorAttached.transform.Translate(Vector2.up * 100 * Time.deltaTime);
            doorDown = false;
        }
    }

    public void SwitchReleased()
    {
        if (!doorDown)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = buttonClosed;
            doorAttached.gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed;
            doorAttached.transform.Translate(Vector2.down * 100 * Time.deltaTime);
            audioSource.PlayOneShot(audioClose);
            doorDown = true;
        }
    }
}
