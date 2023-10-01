using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunsController : GunsController
{
    float pointGunToPlayerSpeed = 5f;

    EnemyController enemyController;
    Transform playerTransform;

    void Start()
    {
        base.Start();

        if (currentFireGun)
        {
            currentFireGun.UpdateAutoShot(true);
        }

        enemyController = GetComponent<EnemyController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        PlayerDistanceAction playerDistanceAction = enemyController.GetPlayerDistanceAction();

        if (playerDistanceAction != PlayerDistanceAction.stopped)
        {
            PointGunToPlayer();
            FireGunAutoShot();
        }
    }

    void FireGunAutoShot()
    {
        if (currentFireGun)
        {
            currentFireGun.AutoShot();
        }
    }

    void PointGunToPlayer()
    {
        var lookToPlayerRotation = Quaternion.LookRotation(
            playerTransform.position - gunsWrapper.transform.position
        );

        gunsWrapper.transform.rotation = Quaternion.Slerp(
            gunsWrapper.transform.rotation,
            lookToPlayerRotation,
            Time.deltaTime * pointGunToPlayerSpeed
        );
    }
}
