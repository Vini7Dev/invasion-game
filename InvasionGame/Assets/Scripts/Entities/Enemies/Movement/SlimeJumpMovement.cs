using System.Collections;
using UnityEngine;

public class SlimeJumpMovement : EnemyMovement
{
    public AudioClip slimeJumpSound;
    public float timeToJump = 0.1f;

    bool jump, stopped = true, inDelay;

    protected override void Stopped()
    {
        speed = 0;
    }

    protected override void WalkToPlayer()
    {
        if (!jump && stopped)
        {
            speed = 0;
            if (!inDelay) StartCoroutine(JumpDelay());
        }
        else if (jump && !stopped)
        {
            speed = enemySkills.moveSpeed;
            if (!inDelay) StartCoroutine(StoppedDelay());
        }

        enemyAnimator.SetBool("IsJumping", jump && !stopped);
    }

    IEnumerator JumpDelay()
    {
        inDelay = true;
        yield return new WaitForSeconds(timeToJump);
        jump = true;
        stopped = false;
        inDelay = false;
    }

    IEnumerator StoppedDelay()
    {
        inDelay = true;
        enemyController.PlaySound(slimeJumpSound, 0.5f);
        yield return new WaitForSeconds(timeToJump);
        stopped = true;
        jump = false;
        inDelay = false;
    }
}
