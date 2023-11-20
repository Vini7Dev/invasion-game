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

        base.Start();
    }

    protected override void WhenDying()
    {
        GameObject levelControllerObj = GameObject.FindGameObjectWithTag("GameController");
        LevelController levelController = levelControllerObj.GetComponent<LevelController>();
        levelController.OnEnemyDies();

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
