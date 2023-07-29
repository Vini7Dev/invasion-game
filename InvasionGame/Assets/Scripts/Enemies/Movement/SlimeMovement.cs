using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : EnemyMovement
{
    public float timeToJump = 0.1f;

    float jumpTimer, movementTime;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        CheckDistanceToMove(Stopped, WalkToPlayer, MoveAwaiFromPlayer);
    }
    
    void Stopped()
    {
        enemyController.enemyAnimator.SetFloat("Speed", 0);
    }

    void WalkToPlayer()
    {
        if (jumpTimer < timeToJump && movementTime == 0)
        {
            jumpTimer += Time.deltaTime;
        }
        else if (movementTime < timeToJump)
        {
            movementTime += Time.deltaTime;

            characterController.Move(GetDirectionAndSpeedMovement(walkSpeed));
        } else {
            jumpTimer = 0;
            movementTime = 0;
        }

        enemyController.enemyAnimator.SetFloat("Speed", timeToJump * 2);
    }
}
