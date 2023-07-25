using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunsController : GunsController
{
    public int enemyGunIndex = 0;

    float autoShotTimer = 0;
    
    void Start()
    {
        GetGunObjects();
        SwitchCurrentGun(enemyGunIndex);
    }

    void Update()
    {
        FireGunAutoShot();
    }

    void FireGunAutoShot()
    {
        if (currentFireGun)
        {
            return;
        }

        if (autoShotTimer <= currentFireGun.timeToShot)
        {
            autoShotTimer += Time.deltaTime;
        }
        else
        {
            currentFireGun.Shot();
            autoShotTimer = 0;
        }
    }
}
