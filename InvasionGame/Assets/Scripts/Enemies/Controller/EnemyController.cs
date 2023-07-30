using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDistanceAction
{
    stopped,
    advancing,
    retreating,
}

public class EnemyController : MonoBehaviour
{
    public bool playerIsAlive;
    public float maxPlayerDistance = 10, minPlayerDistance = 5;
    public Transform playerTransform;
    public GameObject enemySpriteObject;
    public Animator enemyAnimator;
    
    int life = 100;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = enemySpriteObject.GetComponent<Animator>();
    }

    void Update()
    {
        VerifyIfPlayerIsAlive();
    }

    void VerifyIfPlayerIsAlive()
    {
        playerIsAlive = playerTransform.gameObject
            .GetComponent<PlayerController>()
            .IsAlive();
    }

    public void HaveHitADamage(int damageReceived)
    {
        life -= damageReceived;

        if (life > 0)
        {
            return;
        }

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        
        gameController.GetComponent<LevelController>().AddOneOfKilledEnemy();

        gameObject.SetActive(false);
    }

    public float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    public PlayerDistanceAction GetPlayerDistanceAction()
    {
        if (!playerIsAlive)
        {
            return PlayerDistanceAction.stopped;
        }

        if (GetPlayerDistance() <= maxPlayerDistance)
        {
            if (GetPlayerDistance() > minPlayerDistance)
            {
                return PlayerDistanceAction.advancing;
            }
            else
            {
                return PlayerDistanceAction.retreating;
            }
        }
        else
        {
            return PlayerDistanceAction.stopped;
        }
    }
}
