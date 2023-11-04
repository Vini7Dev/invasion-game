using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireGunHands : EntityFireGunHands
{
    float rotateGunSpeed = 3f;
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
        fireGunLeftHand.transform.rotation = RotateGunHand(fireGunLeftHand);
        fireGunRightHand.transform.rotation = RotateGunHand(fireGunRightHand);
    }

    Quaternion RotateGunHand(GameObject gunHand)
    {
        Vector3 directionToPlayer = playerTransform.position - gunHand.transform.position;

        Quaternion rotationToPlayer = Quaternion.LookRotation(
            directionToPlayer,
            Vector3.up
        );

        return Quaternion.Slerp(
            gunHand.transform.rotation,
            rotationToPlayer,
            Time.deltaTime * rotateGunSpeed
        );
    }
}
