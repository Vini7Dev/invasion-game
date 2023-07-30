using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int floorNumber;
    public bool hasFloorKey;

    public Color floorColor;
    public Color wallColor;
    public Material floorMaterial;
    public Material wallMaterial;

    int totalOfFloorEnemies, totalOfKilledEnemies = 0;
    GameObject floorKey;

    void Start()
    {
        floorMaterial.color = floorColor;
        wallMaterial.color = wallColor;

        totalOfFloorEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        floorKey = GameObject.FindGameObjectWithTag("FloorKey");
        floorKey.SetActive(false);

        BuildFloor();
    }

    void BuildFloor()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Transform playerSpawnTransform = GameObject.Find("PlayerSpawn").transform;
        playerTransform.position = playerSpawnTransform.position;
    }

    public void AddOneOfKilledEnemy()
    {
        totalOfKilledEnemies += 1;

        if (totalOfKilledEnemies >= totalOfFloorEnemies)
        {
            floorKey.SetActive(true);
        }
    }
}
