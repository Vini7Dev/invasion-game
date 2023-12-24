using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerDistanceAction
{
    stopped,
    advancing,
    retreating,
}

[System.Serializable]
public class ExecuteOnDeath : UnityEvent
{
}

public class EnemyController : EntityController
{
    public float maxPlayerDistance = 10, minPlayerDistance = 1;
    public ExecuteOnDeath executeOnDeath;

    bool attackStarted;
    Animator enemyAnimator;
    Transform playerTransform;
    LevelController levelController;

    void Start()
    {
        base.Start();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;

        GameObject levelControllerObj = GameObject.FindGameObjectWithTag("GameController");
        levelController = levelControllerObj.GetComponent<LevelController>();
        levelController.IncrementTotalOfEnemiesOnRoom();
    }

    protected override void WhenDying()
    {
        executeOnDeath.Invoke();

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
