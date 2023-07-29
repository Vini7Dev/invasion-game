using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float walkSpeed = 5;

    protected EnemyController enemyController;
    protected CharacterController characterController;
    protected delegate void MovementDelegate();

    protected void Start()
    {
        enemyController = GetComponent<EnemyController>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        CheckDistanceToMove(WalkToPlayer, MoveAwaiFromPlayer);
    }

    protected void CheckDistanceToMove(
        MovementDelegate WalkToPlayerDelegate,
        MovementDelegate MoveAwaiFromPlayerDelegate
    ) {
        PlayerDistanceAction playerDistanceAction = enemyController.GetPlayerDistanceAction();

        if (playerDistanceAction == PlayerDistanceAction.stopped)
        {
            return;
        }

        if (playerDistanceAction == PlayerDistanceAction.advancing)
        {
            WalkToPlayerDelegate();
        }
        else
        {
            MoveAwaiFromPlayerDelegate();
        }
    }

    protected void WalkToPlayer()
    {
        characterController.Move(GetDirectionAndSpeedMovement(walkSpeed));
    }

    protected void MoveAwaiFromPlayer()
    {
        float goBackSpeed = -(walkSpeed - 2);    
    
        characterController.Move(GetDirectionAndSpeedMovement(goBackSpeed));
    }

    protected Vector3 GetDirectionAndSpeedMovement(float speed)
    {
        Vector3 playerPosition = enemyController.playerTransform.position;
        Vector3 moveDirection = playerPosition - transform.position;

        if (moveDirection.magnitude <= 0.1f)
        {
            return Vector3.zero;
        }

        moveDirection.Normalize();

        return moveDirection * Time.deltaTime * speed;
    }
}
