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
    public int life = 100;
    public float maxPlayerDistance = 10, minPlayerDistance = 5;
    public Transform playerTransform;
    public GameObject enemySpriteObject;
    public Animator enemyAnimator;

    float damageTimer, damageTime = 0.2f;
    SpriteRenderer enemySprite;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = enemySpriteObject.GetComponent<Animator>();
        enemySprite = enemySpriteObject.GetComponent<SpriteRenderer>();
        damageTimer = damageTime;
    }

    void Update()
    {
        VerifyIfPlayerIsAlive();
        ReloadDamageTimer();
    }

    void ReloadDamageTimer()
    {
        if (damageTimer <= damageTime)
        {
            enemySprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            damageTimer += Time.deltaTime;
        }
        else
        {
            enemySprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void VerifyIfPlayerIsAlive()
    {
        playerIsAlive = playerTransform.gameObject
            .GetComponent<PlayerController>()
            .IsAlive();
    }

    public void HaveHitADamage(int damageReceived)
    {
        if (damageTimer < damageTime)
        {
            return;
        }

        damageTimer = 0;
        life -= damageReceived;

        if (life <= 0)
        {
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
            gameController.GetComponent<LevelController>().AddOneOfKilledEnemy();
            gameObject.SetActive(false);
        }
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
