using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceUtil : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound(AudioClip soundClip, float timeToDestroy = 5)
    {
        audioSource.clip = soundClip;

        audioSource.Play();

        Destroy(gameObject, timeToDestroy);
    }
}
