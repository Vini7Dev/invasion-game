using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int floorNumber;
    public bool hasFloorKey;

    public TransitionController transitionController;

    public Color floorColor;
    public Color wallColor;
    public Material floorMaterial;
    public Material wallMaterial;

    public GameObject[] floorLevels;

    int totalOfFloorEnemies, totalOfKilledEnemies = 0;
    float startLevelDelay = 0.2f;
    GameObject playerObject, floorKey;

    void Start()
    {
        floorMaterial.color = floorColor;
        wallMaterial.color = wallColor;
        playerObject = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(StartNextLevel());
    }

    public void AddOneOfKilledEnemy()
    {
        totalOfKilledEnemies += 1;

        if (totalOfKilledEnemies >= totalOfFloorEnemies)
        {
            floorKey.SetActive(true);
        }
    }

    public void FinishFloor()
    {
        StartCoroutine(StartNextLevel());
    }

    void ResetFloor()
    {
        floorNumber+= 1;
        hasFloorKey = false;

        playerObject.SetActive(false);

        GameObject[] scenaryObjects = GameObject.FindGameObjectsWithTag("Scenary");

        for (int i = 0; i < scenaryObjects.Length; i++)
        {
            Destroy(scenaryObjects[i]);
        }
    }

    void BuildFloor()
    {
        Instantiate(
            floorLevels[0],
            Vector3.zero,
            Quaternion.identity
        );

        totalOfFloorEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        floorKey = GameObject.FindGameObjectWithTag("FloorKey");
        floorKey.SetActive(false);

        Transform playerSpawnTransform = GameObject.Find("PlayerSpawn").transform;

        Transform mainCameraTransform = Camera.main.gameObject.transform;
        mainCameraTransform.position = new Vector3(
            playerSpawnTransform.position.x,
            mainCameraTransform.position.y,
            playerSpawnTransform.position.z
        );

        playerObject.transform.position = playerSpawnTransform.position;
        playerObject.SetActive(true);
    }

    IEnumerator StartNextLevel()
    {
        transitionController.open = false;
        yield return new WaitForSeconds(startLevelDelay);
        
        ResetFloor();
        BuildFloor();

        yield return new WaitForSeconds(startLevelDelay / 2);

        transitionController.open = true;
    }
}
