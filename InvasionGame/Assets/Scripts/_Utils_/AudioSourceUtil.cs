using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceUtil : MonoBehaviour
{
    public float timeToDestroy = 5;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
