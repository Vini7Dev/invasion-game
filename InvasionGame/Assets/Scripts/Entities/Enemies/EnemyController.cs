using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDistanceAction
{
    stopped,
    advancing,
    retreating,
}

public class EnemyController : EntityController
{
    public float maxPlayerDistance = 10, minPlayerDistance = 1;

    bool attackStarted;
    Animator enemyAnimator;
    Transform playerTransform;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
    }

    void Update()
    {
        base.Update();
    }

    protected override void WhenDying()
    {
        GameObject roomObject = transform.parent.gameObject;

        RoomController roomController = roomObject.GetComponent<RoomController>();
        roomController.OnEnemyDies();

        Destroy(gameObject);
    }

    public float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    public PlayerDistanceAction GetPlayerDistanceAction()
    {
        if (!PlayerController.IsAlive())
        {
            return PlayerDistanceAction.stopped;
        }

        if (attackStarted || GetPlayerDistance() <= maxPlayerDistance)
        {
            attackStarted = true;

            if (GetPlayerDistance() < minPlayerDistance)
            {
                return PlayerDistanceAction.retreating;
            }
            else
            {
                return PlayerDistanceAction.advancing;
            }
        }
        else
        {
            return PlayerDistanceAction.stopped;
        }
    }
}
