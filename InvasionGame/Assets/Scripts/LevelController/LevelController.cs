using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public CameraController cameraController;
    public HUDController hudController;
    public GameObject audioSourceUtil;
    public AudioClip roomTransitionSound;

    int money;
    bool isInRoomTransition;
    GameObject currentRoom;
    LevelBuilder levelBuilder;

    void Start()
    {
        levelBuilder = GetComponent<LevelBuilder>();
        levelBuilder.CreateLevelRooms();
        currentRoom = levelBuilder.GetLevelRoom(0);

        UpdateRoomIsActive(currentRoom, true);

        GameObject mainCamera = Camera.main.gameObject;
    }

    void UpdateRoomIsActive(GameObject roomObject, bool newIsActive)
    {
        roomObject.GetComponent<RoomController>().SetIsRoomActive(newIsActive);
    }

    void PlaySound(AudioClip soundClip, float timeToDestroy = 5)
    {
        GameObject audioInstance = Instantiate(
            audioSourceUtil,
            audioSourceUtil.transform.position,
            audioSourceUtil.transform.rotation
        );

        AudioSourceUtil audioSource = audioInstance.GetComponent<AudioSourceUtil>();
        audioSource.PlaySound(soundClip, timeToDestroy);
    }

    protected IEnumerator DelayToDeactiveRoom(GameObject roomToDeactive)
    {
        yield return new WaitForSeconds(0.25f);

        UpdateRoomIsActive(roomToDeactive, false);

        isInRoomTransition = false;
    }

    public void UpdateCurrentRoom(int roomIndex)
    {
        isInRoomTransition = true;

        PlaySound(roomTransitionSound);

        GameObject previousRoom = currentRoom;

        currentRoom = levelBuilder.GetLevelRoom(roomIndex);

        UpdateRoomIsActive(currentRoom, true);

        Vector2 cameraPosition = new Vector2(
            currentRoom.transform.position.x,
            currentRoom.transform.position.z
        );

        cameraController.UpdateCameraPosition(cameraPosition);

        StartCoroutine(DelayToDeactiveRoom(previousRoom));
    }

    public void IncrementTotalOfEnemiesOnRoom()
    {
        RoomController currentRoomController = currentRoom.GetComponent<RoomController>();
        currentRoomController.IncrementTotalOfEnemiesOnRoom();
    }

    public void OnEnemyDies()
    {
        RoomController currentRoomController = currentRoom.GetComponent<RoomController>();
        currentRoomController.OnEnemyDies();
    }

    public bool GetIsInRoomTransition()
    {
        return isInRoomTransition;
    }

    public void IncrementMoneyValue(int moneyToAdd)
    {
        money += moneyToAdd;

        hudController.UpdateMoneyInfo(money);
    }
}
