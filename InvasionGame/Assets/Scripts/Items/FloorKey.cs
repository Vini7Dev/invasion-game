using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKey : MonoBehaviour
{
    float rotationSpeed = 250;

    void Update()
    {
        transform.Rotate(Vector3.left, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        GameObject.FindGameObjectWithTag("FloorKey").SetActive(false);
    }
}
