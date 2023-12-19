using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject roomPrefab, roomPassagePrefab;
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
            new Vector3(spawnPosition.x, 2, spawnPosition.y),
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

            GameObject currentRoomPassage = CreateRoomPassage(
                nextRoomPosition,
                currentRoomInstance
            );

            GameObject nextRoomPassage = CreateRoomPassage(
                nextRoomPosition,
                nextRoomInstance,
                true
            );

            ConnectPassages(currentRoomPassage, nextRoomPassage);
        }

        return currentRoomInstance;
    }

    GameObject CreateRoomPassage(
        NextRoomPosition nextRoomPosition,
        GameObject roomParent,
        bool reversePosition = false
    )
    {
        Vector2 nextRoomPassagePosition = reversePosition
            ? nextRoomPosition.roomPassagePosition * -1
            : nextRoomPosition.roomPassagePosition;

        Vector3 roomParentPosition = roomParent.transform.position;

        Vector3 roomPassagePosition = new Vector3(
            roomParentPosition.x + nextRoomPassagePosition.x,
            0,
            roomParentPosition.z + nextRoomPassagePosition.y
        );

        Vector3 roomPassageRotation = roomPassagePrefab.transform.rotation.eulerAngles;

        if (!nextRoomPosition.IsDirection(Direction.Up))
        {
            roomPassageRotation.y += 90;
        }

        GameObject roomPassageInstance = Instantiate(
            roomPassagePrefab,
            roomPassagePosition,
            Quaternion.Euler(roomPassageRotation)
        );

        roomPassageInstance.transform.parent = roomParent.transform;

        return roomPassageInstance;
    }

    void ConnectPassages(
        GameObject firstPassage,
        GameObject secondPassage
    )
    {
        firstPassage.GetComponent<PassageController>().passageConnection = secondPassage;
        secondPassage.GetComponent<PassageController>().passageConnection = firstPassage;
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
}
