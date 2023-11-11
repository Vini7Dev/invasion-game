using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    GameObject currentRoom;
    LevelBuilder levelBuilder;
    CameraController cameraController;

    void Start()
    {
        levelBuilder = GetComponent<LevelBuilder>();
        levelBuilder.CreateLevelRooms();

        GameObject mainCamera = Camera.main.gameObject;
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    public void UpdateCurrentRoom(int roomIndex)
    {
        currentRoom = levelBuilder.GetLevelRoom(roomIndex);

        Vector2 cameraPosition = new Vector2(
            currentRoom.transform.position.x,
            currentRoom.transform.position.z
        );

        cameraController.UpdateCameraPosition(cameraPosition);
    }
}
