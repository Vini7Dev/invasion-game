using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDorController : MonoBehaviour
{
    LevelController levelController;

    void Start()
    {
        GameObject levelControllerObject = GameObject.FindGameObjectWithTag("GameController");
        levelController = levelControllerObject.GetComponent<LevelController>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player")
        {
            return;
        }

        if (levelController && levelController.hasFloorKey)
        {
            levelController.FinishFloor();
        }
    }
}
