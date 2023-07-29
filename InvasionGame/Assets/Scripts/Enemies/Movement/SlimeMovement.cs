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
        CheckDistanceToMove(WalkToPlayer, MoveAwaiFromPlayer);
    }

    void WalkToPlayer()
    {
        if (jumpTimer < timeToJump && movementTime == 0)
        {
            jumpTimer += Time.deltaTime;

            enemyController.enemyAnimator.SetFloat("Speed", 0);
        }
        else if (movementTime < timeToJump)
        {
            movementTime += Time.deltaTime;

            enemyController.enemyAnimator.SetFloat("Speed", timeToJump * 4);

            characterController.Move(GetDirectionAndSpeedMovement(walkSpeed));
        } else {
            jumpTimer = 0;
            movementTime = 0;
        }
    }
}
