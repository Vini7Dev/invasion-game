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

    public Vector3 GetPosition()
    {
        return new Vector3(position.x, 0, position.y);
    }

    public Vector3 GetPassagePosition()
    {
        return new Vector3(roomPassagePosition.x, 0, roomPassagePosition.y);
    }
}

public class RoomController : MonoBehaviour
{
    public MeshRenderer roomCover;
    public NextRoomPosition[] nextRoomPositions;
    public GameObject[] roomVariations;
    public int roomIndex;

    bool isRoomActive;
    int totalOfEnemiesOnRoom;
    float roomCoverAnimationSpeed;
    Material roomCoverMaterial;

    void Start()
    {
        roomCoverMaterial = roomCover.materials[0];
        ProcessCurrentRoom();
    }

    void Update()
    {
        OpenOrCloseRoomCover();
    }

    void ProcessCurrentRoom()
    {
        int roomVariationIndex = UnityEngine.Random.Range(0, roomVariations.Length);

        GameObject currentRoomVariation = roomVariations[roomVariationIndex];

        GameObject roomVariationInstance = Instantiate(
            currentRoomVariation,
            transform.position,
            currentRoomVariation.transform.rotation
        );

        RoomVariation roomVariation = roomVariationInstance.GetComponent<RoomVariation>();

        roomVariation.roomController = GetComponent<RoomController>();

        GameObject[] enemies = roomVariationInstance.transform
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
        roomCoverAnimationSpeed = (isRoomActive ? -1 : 1) * Time.deltaTime * 0.5f;
    }

    public bool GetIsRoomActive()
    {
        return isRoomActive;
    }

    public void OnEnemyDies()
    {
        totalOfEnemiesOnRoom -= 1;
    }

    public bool IsAllEnemiesDied()
    {
        return totalOfEnemiesOnRoom <= 0;
    }
}
