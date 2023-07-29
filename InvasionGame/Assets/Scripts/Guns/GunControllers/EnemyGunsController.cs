using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunsController : GunsController
{
    public int enemyGunIndex = 0;

    EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();

        GetGunObjects();
        SwitchCurrentGun(enemyGunIndex);

        if (HasCurrentFireGun())
        {
            currentFireGun.UpdateAutoShot(true);
        }
    }

    void Update()
    {
        base.Update();

        if (enemyController.playerIsAlive)
        {
            FireGunAutoShot();
        }
    }

    bool HasCurrentFireGun()
    {
        return !!currentFireGun;
    }

    void FireGunAutoShot()
    {
        PlayerDistanceAction playerDistanceAction = enemyController.GetPlayerDistanceAction();

        if (playerDistanceAction == PlayerDistanceAction.stopped || !HasCurrentFireGun())
        {
            return;
        }

        currentFireGun.AutoShot();
    }
}
