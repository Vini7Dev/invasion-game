using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKey : MonoBehaviour
{
    float rotationSpeed = 250;
    LevelController levelController;

    void Start()
    {
        GameObject levelControllerObject = GameObject.FindGameObjectWithTag("GameController");
        levelController = levelControllerObject.GetComponent<LevelController>();
    }

    void Update()
    {
        transform.Rotate(Vector3.left, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        levelController.hasFloorKey = true;
        GameObject.FindGameObjectWithTag("FloorKey").SetActive(false);
    }
}