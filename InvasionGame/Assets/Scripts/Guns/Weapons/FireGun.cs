using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotButton
{
    Fire1,
    Fire2
}

public class FireGun : Weapon
{
    public int bullets, maxBullets = 10;
    public float timeToReload = 1;
    public ShotButton shotButton = ShotButton.Fire1;
    public Transform bulletSpawn;
    public GameObject projectileContainerObject;

    protected bool autoShot = false, isSecondaryGun = false, reloading, inDelayShot;
    protected Quaternion projectileRotation;

    protected void Awake()
    {
        base.isFiregun = true;
        bullets = maxBullets;
    }

    protected void Update()
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

            if (Input.GetButton(shotButton.ToString()))
            {
                StartCoroutine(ShotTime());

                InstantiateProjectile();

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

            InstantiateProjectile();

            bullets -= 1;
        }
    }

    void Reload()
    {
        if (!reloading && timeToReload >= 0)
        {
            StartCoroutine(ReloadTime());
        }
    }

    public void UpdateAutoShot(bool newAutoShot)
    {
        autoShot = newAutoShot;
    }

    public void InstantiateProjectile()
    {
        GameObject projectileContainer = Instantiate(
            projectileContainerObject,
            bulletSpawn.position,
            !isSecondaryGun ? transform.rotation : projectileRotation
        );

        projectileContainer.GetComponent<ProjectileContainer>().DefineProjectileProps(
            isPlayerAttack,
            minDamage,
            maxDamage
        );
    }

    IEnumerator ReloadTime()
    {
        reloading = true;
        yield return new WaitForSeconds(timeToReload);
        bullets = maxBullets;
        reloading = false;
    }

    IEnumerator ShotTime()
    {
        inDelayShot = true;
        yield return new WaitForSeconds(attackTime);
        inDelayShot = false;
    }
}
