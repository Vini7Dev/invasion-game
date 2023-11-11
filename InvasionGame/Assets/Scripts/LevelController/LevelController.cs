using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    LevelBuilder levelBuilder;

    void Start()
    {
        levelBuilder = GetComponent<LevelBuilder>();
        levelBuilder.CreateLevelRooms();
    }
}
