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
public class NextRoomPositions
{
    public Direction direction = Direction.Up;
    public Vector2 position = new Vector2(0, 0);
}

public class RoomController : MonoBehaviour
{
    public NextRoomPositions[] nextRoomPositions;

    int totalOfEnemiesOnRoom;

    void Start()
    {
        ProcessCurrentRoom();
    }

    void ProcessCurrentRoom()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        totalOfEnemiesOnRoom = enemies.Count(enemy => enemy.activeSelf);
    }

    public void OnEnemyDies()
    {
        totalOfEnemiesOnRoom -= 1;
    }
}
