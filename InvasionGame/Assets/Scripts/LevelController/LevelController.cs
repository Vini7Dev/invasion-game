using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject[] roomPrefabs, roomPassagePrefabs;

    int roomPerLevel = 5;
    List<GameObject> levelRooms = new List<GameObject>();
    Vector3 currentSpawnerPosition = new Vector3(0, 2, 0);

    void Start()
    {
        CreateLevelRooms();
    }

    void CreateLevelRooms()
    {
        NextRoomPositions spawnDirection, lastSpawnDirection = null;

        for (int roomIndex = 0; roomIndex < roomPerLevel; roomIndex++)
        {
            GameObject roomInstance = Instantiate(
                roomPrefabs[0],
                currentSpawnerPosition,
                Quaternion.identity
            );

            levelRooms.Add(roomInstance);

            RoomController roomController = roomInstance.GetComponent<RoomController>();

            while (true)
            {
                int randomDirectionIndex = UnityEngine.Random.Range(0, 3);
                spawnDirection = roomController.nextRoomPositions[randomDirectionIndex];

                if (lastSpawnDirection == null ||
                    spawnDirection.direction == Direction.Up ||
                    (spawnDirection.direction == Direction.Left && lastSpawnDirection.direction != Direction.Right) ||
                    (spawnDirection.direction == Direction.Right && lastSpawnDirection.direction != Direction.Left))
                {
                    break;
                }
            }

            Vector3 newCurrentSpawnerPosition = new Vector3(
                spawnDirection.position.x, 0, spawnDirection.position.y
            );

            GameObject roomPassage = roomPassagePrefabs[0];

            Vector3 roomPassagePosition = new Vector3(
                spawnDirection.roomPassagePosition.x, -2, spawnDirection.roomPassagePosition.y
            );

            Vector3 roomPassageRotation = roomPassage.transform.rotation.eulerAngles;

            if (spawnDirection.direction == Direction.Left) roomPassageRotation.y -= 90;
            else if (spawnDirection.direction == Direction.Right) roomPassageRotation.y += 90;

            Instantiate(
                roomPassage,
                currentSpawnerPosition + roomPassagePosition,
                Quaternion.Euler(roomPassageRotation)
            );

            currentSpawnerPosition += newCurrentSpawnerPosition;

            lastSpawnDirection = spawnDirection;
        }
    }
}
