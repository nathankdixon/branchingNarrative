using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotSoundController : MonoBehaviour
{
    public static GunShotSoundController instance;

    public AudioSource audioSource;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayGunSound()
    {
        audioSource.Play();
    }
}
