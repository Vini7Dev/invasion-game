using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunsController : GunsController
{
    public WeaponName weaponName = WeaponName.Sword;
    
    float pointGunToPlayerSpeed = 5f;

    EnemyController enemyController;
    Transform playerTransform;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();

        GetGunObjects();
        GetSecondaryGunObjects();
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
