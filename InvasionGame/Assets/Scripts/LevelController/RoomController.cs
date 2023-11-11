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
    public NextRoomPosition[] nextRoomPositions;

    int totalOfEnemiesOnRoom;

    void Start()
    {
        ProcessCurrentRoom();
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

    public void OnEnemyDies()
    {
        totalOfEnemiesOnRoom -= 1;
    }

    public bool IsAllEnemiesDied()
    {
        return totalOfEnemiesOnRoom <= 0;
    }
}
