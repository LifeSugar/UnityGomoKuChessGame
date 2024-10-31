using System;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    private static MusicManagerScript instance;
    public AudioSource audioSource;
    [Range(0, 1)] public float volume;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}