using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : EnemyMovement
{
    public float timeToJump = 0.1f;

    float jumpTimer, movementTime;
    bool jump, stopped = true;

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
        if (!jump && stopped)
        {
            StartCoroutine(JumpDelay());
        }
        else if (jump && !stopped)
        {
            characterController.Move(GetDirectionAndSpeedMovement(walkSpeed));
            StartCoroutine(StoppedDelay());
        }

        enemyController.enemyAnimator.SetFloat("Speed", timeToJump * 2);
    }

    IEnumerator JumpDelay()
    {
        jump = false;
        stopped = true;
        yield return new WaitForSeconds(timeToJump);
        jump = true;
        stopped = false;
    }

    IEnumerator StoppedDelay()
    {
        stopped = false;
        jump = true;
        yield return new WaitForSeconds(timeToJump);
        stopped = true;
        jump = false;
    }
}
