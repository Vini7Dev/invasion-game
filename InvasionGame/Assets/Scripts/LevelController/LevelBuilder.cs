using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject roomPrefab;
    public int levelRoomsCount = 10;

    int roomCount, bifurcationsCount, maxBifurcations = 2;
    Vector2[] roomPositions;

    void Start()
    {
        roomPositions = new Vector2[levelRoomsCount];

        CreateLevelRoom(new Vector2(0, 0), levelRoomsCount - 1, "ROOT");
    }

    GameObject CreateLevelRoom(
        Vector2 spawnPosition,
        int childCount,
        string objectTreeName
    )
    {
        GameObject currentRoomInstance = Instantiate(
            roomPrefab,
            new Vector3(spawnPosition.x, 0, spawnPosition.y),
            Quaternion.identity
        );

        currentRoomInstance.name = objectTreeName;

        roomPositions[roomCount] = spawnPosition;
        roomCount += 1;

        RoomController currentRoomController = currentRoomInstance.GetComponent<RoomController>();

        bool canCreateBifurcation =
            bifurcationsCount < maxBifurcations
            && roomCount > 1
            && roomCount < (levelRoomsCount - 1);

        bool willCreateBifurcation = canCreateBifurcation && UnityEngine.Random.Range(0, 10) > 6;

        int roomChildCount = willCreateBifurcation ? 1 : 2;

        int maxSecondWayChildCount = childCount - 3 > 0 ? childCount : 0;

        int secondWayChildCount = willCreateBifurcation ? 0 : UnityEngine.Random.Range(0, maxSecondWayChildCount);

        int firstWayCildCount = childCount - secondWayChildCount;

        for (int childIndex = 0; childIndex < roomChildCount; childIndex++)
        {
            if (childIndex > 0) bifurcationsCount += 1;

            int wayChildCount = childIndex == 0 ? firstWayCildCount : secondWayChildCount;

            if (wayChildCount <= 0) continue;

            NextRoomPosition nextRoomPosition = GetRamdomNextRoomPosition(
                currentRoomController,
                spawnPosition
            );

            if (nextRoomPosition == null) continue;

            Vector2 newSpawnerPosition = spawnPosition + nextRoomPosition.position;

            string childObjectTreeName = objectTreeName + " - " + childIndex;

            GameObject nextRoomInstance = CreateLevelRoom(
                newSpawnerPosition,
                wayChildCount - 1,
                childObjectTreeName
            );

            // nextRoomInstance
        }

        return currentRoomInstance;
    }

    NextRoomPosition GetRamdomNextRoomPosition(
        RoomController currentRoomController,
        Vector2 spawnPosition
    )
    {
        int maxAttempts = 100;

        for (int attempts = 0; attempts < maxAttempts; attempts++)
        {
            int directionIndex = UnityEngine.Random.Range(0, 3);

            NextRoomPosition nextRoomPosition = currentRoomController.nextRoomPositions[directionIndex];

            if (FindRoomPosition(spawnPosition + nextRoomPosition.position)) continue;

            return nextRoomPosition;
        }

        return null;
    }

    bool FindRoomPosition(Vector2 targetPosition)
    {
        return System.Array.Exists(
            roomPositions,
            position => position == targetPosition
        );
    }

    public void CreateLevelRooms() {}
    public GameObject GetLevelRoom(int roomIndex) { return gameObject; }

    /*
    public GameObject[] roomPrefabs, roomPassagePrefabs;
    public int roomPerLevel = 2;

    int passagesCount;
    GameObject[] levelRooms, levelPassages;

    public GameObject GetLevelRoom(int roomIndex)
    {
        return levelRooms[roomIndex];
    }

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

            roomInstance.GetComponent<RoomController>().roomIndex = roomIndex;

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
    */
}
