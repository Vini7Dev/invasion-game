using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float walkSpeed = 5, lookSpeed = 5;

    EnemyController enemyController;
    CharacterController characterController;
    Vector3 lookDirection;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        characterController = GetComponent<CharacterController>();
        lookDirection = transform.position;
    }

    float GetPlayerDistance()
    {
        return Vector3.Distance(
            transform.position,
            enemyController.playerTransform.position
        );
    }

    void Update()
    {
        PlayerDistanceAction playerDistanceAction = enemyController.GetPlayerDistanceAction();

        if (!enemyController.playerIsAlive || playerDistanceAction == PlayerDistanceAction.stopped)
        {
            return;
        }

        LookToPlayer();

        if (playerDistanceAction == PlayerDistanceAction.advancing)
        {
            WalkToPlayer();
        }
        else
        {
            MoveAwaiFromPlayer();
        }
    }

    void LookToPlayer()
    {
        lookDirection = Vector3.Lerp(
            lookDirection,
            enemyController.playerTransform.position,
            Time.deltaTime * lookSpeed
        );

        transform.LookAt(lookDirection); 
    }

    Vector3 GetDirectionToMove(float walkSpeed)
    {
        return transform.forward * Time.deltaTime * walkSpeed;
    }

    void WalkToPlayer()
    {
        characterController.Move(GetDirectionToMove(walkSpeed));
    }

    void MoveAwaiFromPlayer()
    {
        float goBackSpeed = -(walkSpeed - 2);    
    
        characterController.Move(GetDirectionToMove(goBackSpeed));
    }
}
