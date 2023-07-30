using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : Weapon
{
    public int bullets, maxBullets = 10;
    public float timeToReload = 1;
    public Transform bulletSpawn;
    public GameObject bulletsContainerObject;

    bool autoShot = false;
    float shotTimer, reloadTimer;

    void Awake()
    {
        base.isFiregun = true;
        bullets = maxBullets;
    }

    void Update()
    {
        Shot();
    }

    public void Shot()
    {
        if(autoShot)
        {
            return;
        }

        if (bullets <= 0)
        {
            Reload();
        }
        else
        {
            if (shotTimer >= attackTime)
            {
                if (Input.GetButton("Fire1"))
                {
                    GameObject bulletsContainer = Instantiate(
                        bulletsContainerObject,
                        bulletSpawn.position,
                        transform.rotation
                    );

                    bulletsContainer.GetComponent<BulletsContainer>().DefineBulletsProps(
                        isPlayerAttack,
                        minDamage,
                        maxDamage
                    );

                    bullets -= 1;
                    shotTimer = 0;
                }
            }
            else
            {
                shotTimer += Time.deltaTime;
            }
        }
    }

    public void AutoShot()
    {
        if (bullets <= 0)
        {
            Reload();
        }
        else
        {
            if (shotTimer >= attackTime)
            {
                GameObject bullet = Instantiate(
                    bulletsContainerObject,
                    bulletSpawn.position,
                    transform.rotation
                );

                bullet.GetComponent<BulletsContainer>().DefineBulletsProps(
                    isPlayerAttack,
                    minDamage,
                    maxDamage
                );
                bullets -= 1;
                shotTimer = 0;
            }
            else
            {
                shotTimer += Time.deltaTime;
            }
        }
    }

    void Reload()
    {
        if (reloadTimer < timeToReload)
        {
            reloadTimer += Time.deltaTime;
        }
        else
        {
            bullets = maxBullets;
            reloadTimer = 0;
        }
    }

    public void UpdateAutoShot(bool newAutoShot)
    {
        autoShot = newAutoShot;
    }
}
