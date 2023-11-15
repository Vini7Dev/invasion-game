using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator enemyAnimator;
    public float retreatingSpeed;

    protected CharacterController characterController;
    protected EnemyController enemyController;
    protected Transform playerTransform;
    protected EntitySkills enemySkills;
    protected delegate void MovementDelegate();

    protected void Start()
    {
        characterController = GetComponent<CharacterController>();
        enemyController = GetComponent<EnemyController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemySkills = GetComponent<EntitySkills>();
        retreatingSpeed = enemySkills.moveSpeed - 2;
    }

    void Update()
    {
        CheckDistanceToMove(Stopped, WalkToPlayer, MoveAwaiFromPlayer);
    }

    protected void CheckDistanceToMove(
        MovementDelegate StoppedDelegate,
        MovementDelegate WalkToPlayerDelegate,
        MovementDelegate MoveAwaiFromPlayerDelegate
    ) {
        PlayerDistanceAction playerDistanceAction = enemyController.GetPlayerDistanceAction();

        if (playerDistanceAction == PlayerDistanceAction.stopped)
        {
            StoppedDelegate();
        }
        else if (playerDistanceAction == PlayerDistanceAction.advancing)
        {
            WalkToPlayerDelegate();
        }
        else
        {
            MoveAwaiFromPlayerDelegate();
        }
    }

    protected void Stopped() {}

    protected void WalkToPlayer()
    {
        MoveWithPlayerDirectionBySpeed(enemySkills.moveSpeed);
    }

    protected void MoveAwaiFromPlayer()
    {
        MoveWithPlayerDirectionBySpeed(-retreatingSpeed);
    }

    protected void MoveWithPlayerDirectionBySpeed(float speed)
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3 moveDirection = playerPosition - transform.position;

        if (moveDirection.magnitude <= 0.1f)
        {
            return;
        }

        moveDirection.Normalize();

        characterController.Move(moveDirection * Time.deltaTime * speed);
    }
}
