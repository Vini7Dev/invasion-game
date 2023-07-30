using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunsController : GunsController
{
    public WeaponName weaponName = WeaponName.Sword;

    EnemyController enemyController;
    Transform playerTransform;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();

        GetGunObjects();
        SwitchCurrentGun(weaponName);
        
        if (currentFireGun)
        {
            currentFireGun.UpdateAutoShot(true);
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        PlayerDistanceAction playerDistanceAction = enemyController.GetPlayerDistanceAction();

        if (playerDistanceAction != PlayerDistanceAction.stopped)
        {
            PointGunsToMouse();
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

    void PointGunsToMouse()
    {
        gunsWrapper.transform.LookAt(playerTransform.position);
    }
}