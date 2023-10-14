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
    PlayerController playerController;
    Transform playerTransform;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
        playerController = playerObject.GetComponent<PlayerController>();
    }

    bool VerifyIfPlayerIsAlive()
    {
        return playerController.IsAlive();
    }

    public float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    public PlayerDistanceAction GetPlayerDistanceAction()
    {
        if (!VerifyIfPlayerIsAlive())
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
