using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    public bool friendlyBullet = false;
    public int bullets, maxBullets = 10, minDamage = 10, maxDamage = 25;
    public float timeToShot = 0.5f, timeToReload = 1;
    public Transform bulletSpawn;
    public GameObject bulletsContainerObject;
    bool autoShot = false;
    float shotTimer, reloadTimer;

    void Start()
    {
        bullets = maxBullets;
    }

    void Update()
    {
        Shot();
    }

    public void Shot()
    {
        if(!autoShot)
        {
            if (bullets <= 0)
            {
                Reload();
            }
            else
            {
                if (shotTimer >= timeToShot)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        GameObject bulletsContainer = Instantiate(
                            bulletsContainerObject,
                            bulletSpawn.position,
                            transform.rotation
                        );

                        bulletsContainer.GetComponent<BulletsContainer>().DefineBulletsProps(
                            friendlyBullet,
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
    }

    public void AutoShot()
    {
        if (bullets <= 0)
        {
            Reload();
        }
        else
        {
            if (shotTimer >= timeToShot)
            {
                GameObject bullet = Instantiate(
                    bulletsContainerObject,
                    bulletSpawn.position,
                    transform.rotation
                );

                bullet.GetComponent<BulletsContainer>().DefineBulletsProps(
                    friendlyBullet,
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
