using System.Collections;
using UnityEngine;

public class SlimeJumpMovement : EnemyMovement
{
    public float timeToJump = 0.1f;

    bool jump, stopped = true;

    protected override void Stopped()
    {
        speed = 0;
    }

    protected override void WalkToPlayer()
    {
        if (!jump && stopped)
        {
            speed = 0;
            StartCoroutine(JumpDelay());
        }
        else if (jump && !stopped)
        {
            speed = enemySkills.moveSpeed;
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
