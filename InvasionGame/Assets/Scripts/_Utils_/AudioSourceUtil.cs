using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceUtil : MonoBehaviour
{
    public AudioSource audioSource;
    public float timeToDestroy = 5;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    public void PlaySound(AudioClip soundClip)
    {
        audioSource.clip = soundClip;
        audioSource.Play();
    }
}
