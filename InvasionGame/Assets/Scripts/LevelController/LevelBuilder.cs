using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject[] roomPrefabs, roomPassagePrefabs;

    int roomPerLevel = 5;
    List<GameObject> levelRooms = new List<GameObject>();

    void Start()
    {
        CreateLevelRooms();
    }

    void CreateLevelRooms()
    {
        NextRoomPosition previousRoomPosition = null;

        Vector3 spawnerPosition = new Vector3(0, 2, 0);

        for (int roomIndex = 0; roomIndex < roomPerLevel; roomIndex++)
        {
            GameObject roomObject = roomPrefabs[0];

            GameObject roomInstance = Instantiate(
                roomObject,
                spawnerPosition,
                Quaternion.identity
            );

            levelRooms.Add(roomInstance);

            NextRoomPosition nextRoomPosition = GetRamdomDirection(
                roomInstance,
                previousRoomPosition
            );

            CreateRoomPassage(roomInstance, nextRoomPosition, spawnerPosition);

            if (previousRoomPosition != null)
            {
                CreateRoomPassage(
                    levelRooms[roomIndex-1],
                    previousRoomPosition,
                    spawnerPosition,
                    true
                );
            }

            previousRoomPosition = nextRoomPosition;
            spawnerPosition += nextRoomPosition.GetPosition();
        }
    }

    NextRoomPosition GetRamdomDirection(
        GameObject currentRoom,
        NextRoomPosition previousRoomPosition
    )
    {
        RoomController roomController = currentRoom.GetComponent<RoomController>();

        while (true)
        {
            int directionIndex = UnityEngine.Random.Range(0, 3);
            NextRoomPosition nextRoomPosition = roomController.nextRoomPositions[directionIndex];

            if (previousRoomPosition == null ||
                nextRoomPosition.IsDirection(Direction.Up) ||
                (nextRoomPosition.IsDirection(Direction.Left) && !previousRoomPosition.IsDirection(Direction.Right)) ||
                (nextRoomPosition.IsDirection(Direction.Right) && !previousRoomPosition.IsDirection(Direction.Left)))
            {
                return nextRoomPosition;
            }
        }
    }

    void CreateRoomPassage(
        GameObject roomParent,
        NextRoomPosition nextRoomPosition,
        Vector3 spawnerPosition,
        bool previousPassage = false
    )
    {
        GameObject roomPassage = roomPassagePrefabs[0];

        Vector3 roomPassagePosition = new Vector3(
            nextRoomPosition.GetPassagePosition().x, -2, nextRoomPosition.GetPassagePosition().z
        );

        if (previousPassage)
        {
            roomPassagePosition *= -1;
            roomPassagePosition.y = -2;
        }

        Vector3 roomPassageRotation = roomPassage.transform.rotation.eulerAngles;

        if (nextRoomPosition.IsDirection(Direction.Left)
            || nextRoomPosition.IsDirection(Direction.Right)
        )
        {
            roomPassageRotation.y += 90;
        }

        GameObject roomPassageInstance = Instantiate(
            roomPassage,
            spawnerPosition + roomPassagePosition,
            Quaternion.Euler(roomPassageRotation)
        );

        roomPassageInstance.transform.parent = roomParent.transform;
    }
}
