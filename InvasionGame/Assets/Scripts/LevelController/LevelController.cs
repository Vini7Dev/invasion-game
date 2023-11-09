using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject[] roomPrefabs;

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

            currentSpawnerPosition += newCurrentSpawnerPosition;

            lastSpawnDirection = spawnDirection;
        }
    }
}
