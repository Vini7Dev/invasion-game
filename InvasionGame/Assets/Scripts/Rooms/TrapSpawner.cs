using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "RoomFloor") return;

        if (objectToSpawn)
        {
            GameObject trapSpawned = Instantiate(
                objectToSpawn,
                transform.position,
                objectToSpawn.transform.rotation
            );

            trapSpawned.transform.parent = transform.parent;
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
