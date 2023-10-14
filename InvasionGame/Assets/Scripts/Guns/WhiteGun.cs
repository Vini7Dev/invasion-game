using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteGun : MonoBehaviour
{
    const string ENEMY_TAG = "Enemy";
    const string PLAYER_TAG = "Player";

    public LayerMask collisionLayer;
    public bool isPlayerAttack;
    public float detectionIntervalPerSecond = 0.5f;

    float distanceToCreateCollision = 0.2f;
    Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;

        StartCoroutine(CollisionDetectionRoutine());
    }

    IEnumerator CollisionDetectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(detectionIntervalPerSecond);

            Vector3 currentPosition = transform.position;
            float distance = Vector3.Distance(previousPosition, currentPosition);

            if (distance > distanceToCreateCollision)
            {
                Vector3 dimensions = new Vector3(
                    Mathf.Abs(previousPosition.x - currentPosition.x),
                    Mathf.Abs(previousPosition.y - currentPosition.y),
                    Mathf.Abs(previousPosition.z - currentPosition.z)
                );

                Vector3 midpoint = (previousPosition + currentPosition) / 2;

                Collider[] colliders = Physics.OverlapBox(midpoint, dimensions / 2, Quaternion.identity, collisionLayer);

                foreach (var collider in colliders)
                {
                    if (collider.CompareTag(isPlayerAttack ? ENEMY_TAG : PLAYER_TAG))
                    {
                        Debug.Log("Detected: " + collider.gameObject.name);
                    }
                }
            }

            previousPosition = currentPosition;
        }
    }
}
