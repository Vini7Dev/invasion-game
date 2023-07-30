using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : Weapon
{
    public int bullets, maxBullets = 10;
    public float timeToReload = 1;
    public Transform bulletSpawn;
    public GameObject bulletsContainerObject;

    bool autoShot = false, reloading, inDelayShot;

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
            if (inDelayShot)
            {
                return;
            }

            if (Input.GetButton("Fire1"))
            {
                StartCoroutine(ShotTime());

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
            if (inDelayShot)
            {
                return;
            }

            StartCoroutine(ShotTime());

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
        }
    }

    void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(ReloadTime());
        }
        else
        {
            bullets = maxBullets;
        }
    }

    public void UpdateAutoShot(bool newAutoShot)
    {
        autoShot = newAutoShot;
    }

    IEnumerator ReloadTime()
    {
        reloading = true;
        yield return new WaitForSeconds(timeToReload);
        reloading = false;
    }

    IEnumerator ShotTime()
    {
        inDelayShot = true;
        yield return new WaitForSeconds(attackTime);
        inDelayShot = false;
    }
}
