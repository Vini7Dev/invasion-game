using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ExecuteOnToggleIsMoving : UnityEvent
{
}

public class EnemyMovementPerTimeInterval : EnemyMovement
{
    public float movementTime = 1, stoppedTime = 1;
    public ExecuteOnToggleIsMoving executeOnToggleIsMoving;

    float actionTimer, actionTimerLimit, smoothSpeed;
    bool isMoving;

    void Start()
    {
        base.Start();
        actionTimerLimit = 1.5f;
        enemyAnimator.speed = 0;
    }

    void Update()
    {
        RunActionTimer();

        if (TimerEnds()) ToggleIsMoving();

        CheckDistanceToMove();
        MoveWithPlayerDirectionBySpeed();
    }

    bool TimerEnds()
    {

        return actionTimer >= actionTimerLimit;
    }

    void RunActionTimer()
    {
        if (TimerEnds()) return;

        actionTimer += Time.deltaTime;
    }

    void ToggleIsMoving()
    {
        isMoving = !isMoving;

        if (isMoving)
        {
            actionTimerLimit = movementTime;
            enemyAnimator.speed = 0.25f;
        }
        else
        {
            actionTimerLimit = stoppedTime;
            enemyAnimator.speed = 0;
        }

        actionTimer = 0;
        executeOnToggleIsMoving.Invoke();
    }

    protected override void CheckDistanceToMove()
    {
        PlayerDistanceAction playerDistanceAction = enemyController.GetPlayerDistanceAction();

        if (!isMoving || playerDistanceAction == PlayerDistanceAction.stopped)
            Stopped();
        else if (isMoving && playerDistanceAction == PlayerDistanceAction.advancing)
            WalkToPlayer();
        else if (isMoving)
            MoveAwaiFromPlayer();
    }

    protected override void Stopped()
    {
        if (smoothSpeed > 0) smoothSpeed -= Time.deltaTime * 4;
        else smoothSpeed = 0;

        speed = smoothSpeed;
    }

    protected override void WalkToPlayer()
    {
        if (smoothSpeed < enemySkills.moveSpeed) smoothSpeed += Time.deltaTime * 4;

        speed = smoothSpeed;
    }

    protected override void MoveAwaiFromPlayer()
    {
        if (smoothSpeed > -retreatingSpeed) smoothSpeed -= Time.deltaTime * 4;

        speed = smoothSpeed;
    }
}
