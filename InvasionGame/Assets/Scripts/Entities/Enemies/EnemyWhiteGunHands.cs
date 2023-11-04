using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWhiteGunHands : EntityWhiteGunHands
{
    Transform playerTransform;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
    }

    void Update()
    {
        PointToPlayer();
    }

    void PointToPlayer()
    {
        if (playerTransform != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(
                playerTransform.position - whiteGunHands.transform.position
            );

            whiteGunHands.transform.rotation = Quaternion.Slerp(
                whiteGunHands.transform.rotation,
                targetRotation,
                Time.deltaTime * 4
            );
        }
    }
}
