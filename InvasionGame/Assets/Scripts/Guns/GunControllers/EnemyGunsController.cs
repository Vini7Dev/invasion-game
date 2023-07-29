using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunsController : GunsController
{
    public int enemyGunIndex = 0;

    EnemyController enemyController;
    Transform playerTransform;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();

        GetGunObjects();
        SwitchCurrentGun(enemyGunIndex);
        
        if (HasCurrentFireGun())
        {
            currentFireGun.UpdateAutoShot(true);
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        base.Update();

        PlayerDistanceAction playerDistanceAction = enemyController.GetPlayerDistanceAction();

        if (playerDistanceAction != PlayerDistanceAction.stopped && HasCurrentFireGun())
        {
            FireGunAutoShot();
            PointGunsToMouse();
        }
    }

    bool HasCurrentFireGun()
    {
        return !!currentFireGun;
    }

    void FireGunAutoShot()
    {
        currentFireGun.AutoShot();
    }

    void PointGunsToMouse()
    {
        gunsWrapper.transform.LookAt(playerTransform.position);
    }
}
