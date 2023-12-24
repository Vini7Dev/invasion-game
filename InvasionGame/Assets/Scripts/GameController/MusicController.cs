using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] gameMusics;
    public int musicIndex = -1;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        int indexOfMusic = musicIndex < 0 || musicIndex >= gameMusics.Length
            ? UnityEngine.Random.Range(0, gameMusics.Length)
            : musicIndex;

        audioSource.clip = gameMusics[indexOfMusic];
        audioSource.Play();

        Debug.Log($"===> Music {indexOfMusic} is playng!");
    }
}
