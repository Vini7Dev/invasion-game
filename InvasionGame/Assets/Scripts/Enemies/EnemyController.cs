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
    public float maxPlayerDistance = 10, minPlayerDistance = 5;
    public Transform playerTransform;
    public bool playerIsAlive;

    int life = 100;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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

        gameObject.SetActive(life > 0);
    }

    public float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    public PlayerDistanceAction GetPlayerDistanceAction()
    {
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
