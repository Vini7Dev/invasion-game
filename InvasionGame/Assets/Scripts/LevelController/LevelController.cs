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
        currentRoom = levelBuilder.GetLevelRoom(0);

        UpdateRoomIsActive(true);

        GameObject mainCamera = Camera.main.gameObject;
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    void UpdateRoomIsActive(bool newIsActive)
    {
        currentRoom.GetComponent<RoomController>().SetIsActive(newIsActive);
    }

    public void UpdateCurrentRoom(int roomIndex)
    {
        UpdateRoomIsActive(false);

        currentRoom = levelBuilder.GetLevelRoom(roomIndex);

        UpdateRoomIsActive(true);

        Vector2 cameraPosition = new Vector2(
            currentRoom.transform.position.x,
            currentRoom.transform.position.z
        );

        cameraController.UpdateCameraPosition(cameraPosition);
    }
}
