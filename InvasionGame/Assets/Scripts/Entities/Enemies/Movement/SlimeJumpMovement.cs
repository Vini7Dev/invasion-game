using System.Collections;
using UnityEngine;

public class SlimeJumpMovement : EnemyMovement
{
    public float timeToJump = 0.1f;

    bool jump, stopped = true;

    void Update()
    {
        CheckDistanceToMove(Stopped, WalkToPlayer, MoveAwaiFromPlayer);
    }

    void Stopped()
    {
        enemyAnimator.SetFloat("Speed", 0);
    }

    void WalkToPlayer()
    {
        if (!jump && stopped)
        {
            StartCoroutine(JumpDelay());
        }
        else if (jump && !stopped)
        {
            MoveWithPlayerDirectionBySpeed(enemySkills.moveSpeed);
            StartCoroutine(StoppedDelay());
        }

        enemyAnimator.SetBool("IsJumping", jump && !stopped);
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
