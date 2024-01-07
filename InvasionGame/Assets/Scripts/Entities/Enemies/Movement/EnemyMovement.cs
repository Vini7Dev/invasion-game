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
    protected float speed;

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
        CheckDistanceToMove();
        MoveWithPlayerDirectionBySpeed();
    }

    protected virtual void CheckDistanceToMove()
    {
        PlayerDistanceAction playerDistanceAction = enemyController.GetPlayerDistanceAction();

        if (playerDistanceAction == PlayerDistanceAction.stopped) Stopped();
        else if (playerDistanceAction == PlayerDistanceAction.advancing) WalkToPlayer();
        else MoveAwaiFromPlayer();
    }

    protected virtual void Stopped()
    {
        speed = 0;
    }

    protected virtual void WalkToPlayer()
    {
        speed = enemySkills.moveSpeed;
    }

    protected virtual void MoveAwaiFromPlayer()
    {
        speed = -retreatingSpeed;
    }

    protected void MoveWithPlayerDirectionBySpeed()
    {
        Vector3 playerPosition = playerTransform.position;
        Vector3 moveDirection = playerPosition - transform.position;

        if (moveDirection.magnitude <= 0.1f) return;

        moveDirection.Normalize();

        characterController.Move(moveDirection * Time.deltaTime * speed);
    }
}
