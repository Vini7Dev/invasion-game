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
    public int roomIndex;

    bool isActive;
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
        GameObject[] enemies = transform
            .Cast<Transform>()
            .Where(child => child.gameObject.activeSelf && child.CompareTag("Enemy"))
            .Select(child => child.gameObject)
            .ToArray();

        totalOfEnemiesOnRoom = enemies.Count(enemy => enemy.activeSelf);
    }

    void OpenOrCloseRoomCover()
    {
        if ((isActive && roomCoverMaterial.color.a > 0)
            || (!isActive && roomCoverMaterial.color.a < 1)
        )
        {
            float currentAlpha = roomCoverMaterial.color.a;
            roomCoverMaterial.color = new Color(0, 0, 0, currentAlpha + roomCoverAnimationSpeed);
        }
        else {
            roomCoverMaterial.color = new Color(0, 0, 0, isActive ? 0 : 1);
        }
    }

    public void SetIsActive(bool newIsActive)
    {
        isActive = newIsActive;
        roomCoverAnimationSpeed = (isActive ? -1 : 1) * Time.deltaTime * 0.5f;
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
