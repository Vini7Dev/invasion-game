using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum Direction
{
    Up,
    Left,
    Right
}

[Serializable]
public class NextRoomPosition
{
    public Direction direction = Direction.Up;
    public Vector2 position, roomPassagePosition = new Vector2(0, 0);

    public bool IsDirection(Direction directionToCheck)
    {
        return direction == directionToCheck;
    }
}

public class RoomController : MonoBehaviour
{
    public MeshRenderer roomCover;
    public NextRoomPosition[] nextRoomPositions;
    public GameObject[] roomVariations;
    public GameObject sceneryObjects;
    public Animator roomMinimapAnimator;
    public int roomIndex;

    bool isRoomActive;
    int totalOfEnemiesOnRoom;
    float roomCoverAnimationSpeed, roomCoverTransitionSpeed = 0.5f;
    Material roomCoverMaterial;
    GameObject roomVariationObject;

    void Start()
    {
        roomCoverMaterial = roomCover.materials[0];
        ProcessCurrentRoom();
        ToggleActiveOfScenaryAndVariation();
    }

    void Update()
    {
        OpenOrCloseRoomCover();
    }

    void ProcessCurrentRoom()
    {
        int roomVariationIndex = 0;

        if (roomIndex != 0)
        {
            roomVariationIndex = UnityEngine.Random.Range(0, roomVariations.Length);
        }

        GameObject currentRoomVariation = roomVariations[roomVariationIndex];

        GameObject roomVariationInstance = Instantiate(
            currentRoomVariation,
            transform.position,
            currentRoomVariation.transform.rotation
        );

        roomVariationObject = roomVariationInstance;

        RoomVariation roomVariation = roomVariationObject.GetComponent<RoomVariation>();

        roomVariation.roomController = GetComponent<RoomController>();

        GameObject[] enemies = roomVariationObject.transform
            .Find("Enemies")
            .Cast<Transform>()
            .Select(child => child.gameObject)
            .ToArray();

        totalOfEnemiesOnRoom = enemies.Length;
    }

    void OpenOrCloseRoomCover()
    {
        if ((isRoomActive && roomCoverMaterial.color.a > 0)
            || (!isRoomActive && roomCoverMaterial.color.a < 1)
        )
        {
            float currentAlpha = roomCoverMaterial.color.a;

            roomCoverMaterial.color = new Color(0, 0, 0, currentAlpha + roomCoverAnimationSpeed);

            currentAlpha = roomCoverMaterial.color.a;

            if (isRoomActive && currentAlpha < 0)
                roomCoverMaterial.color = new Color(0, 0, 0, 0);
            else if (!isRoomActive && currentAlpha > 1)
                roomCoverMaterial.color = new Color(0, 0, 0, 1);
        }
    }

    public void SetIsRoomActive(bool newIsActive)
    {
        isRoomActive = newIsActive;
        roomCoverAnimationSpeed = (isRoomActive ? -1 : 5) * Time.deltaTime * roomCoverTransitionSpeed;

        if (isRoomActive) ToggleActiveOfScenaryAndVariation();
        else StartCoroutine(HideSceneryObjects());
    }

    void ToggleActiveOfScenaryAndVariation()
    {
        sceneryObjects.SetActive(isRoomActive);
        roomVariationObject.SetActive(isRoomActive);
    }

    void OnAllEnemiesAreKilled()
    {
        roomMinimapAnimator.SetTrigger("RoomCompleted");
    }

    public void OnEnemyDies()
    {
        totalOfEnemiesOnRoom -= 1;

        if (IsAllEnemiesDied()) OnAllEnemiesAreKilled();
    }

    public bool IsAllEnemiesDied()
    {
        return totalOfEnemiesOnRoom <= 0;
    }

    public bool GetIsRoomActive()
    {
        return isRoomActive;
    }

    IEnumerator HideSceneryObjects()
    {
        yield return new WaitForSeconds(roomCoverTransitionSpeed + 0.1f);

        ToggleActiveOfScenaryAndVariation();
    }
}
