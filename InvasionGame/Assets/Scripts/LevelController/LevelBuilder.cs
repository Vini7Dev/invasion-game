using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject[] roomPrefabs, roomPassagePrefabs;
    public int roomPerLevel = 2;

    int passagesCount;
    GameObject[] levelRooms, levelPassages;

    public void CreateLevelRooms()
    {
        levelRooms = new GameObject[roomPerLevel];
        levelPassages = new GameObject[roomPerLevel * 2];

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

            levelRooms[roomIndex] = roomInstance;

            NextRoomPosition nextRoomPosition = GetRamdomDirection(
                roomInstance,
                previousRoomPosition
            );

            if (previousRoomPosition != null)
            {
                CreateRoomPassage(
                    levelRooms[roomIndex],
                    previousRoomPosition,
                    spawnerPosition,
                    true
                );
            }

            CreateRoomPassage(roomInstance, nextRoomPosition, spawnerPosition);

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
        bool isPreviousPassage = false
    )
    {
        GameObject roomPassage = roomPassagePrefabs[0];

        Vector3 roomPassagePosition = new Vector3(
            nextRoomPosition.GetPassagePosition().x, -2, nextRoomPosition.GetPassagePosition().z
        );

        if (isPreviousPassage)
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

        if (isPreviousPassage && levelPassages.Length > 1)
        {
            GameObject previousPassageObject = levelPassages[passagesCount-1];

            roomPassageInstance
                .GetComponent<PassageController>()
                .passageConnection = previousPassageObject;

            previousPassageObject
                .GetComponent<PassageController>()
                .passageConnection = roomPassageInstance;
        }

        levelPassages[passagesCount] = roomPassageInstance;

        passagesCount += 1;
    }
}
