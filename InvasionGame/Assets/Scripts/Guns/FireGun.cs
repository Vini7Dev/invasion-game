using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    public int maxBullets = 10;
    public float timeToShot = 0.2f, timeToReload = 1;
    public Transform bulletSpawn;
    public GameObject bulletObject;

    int bullets;
    float shotTimer, reloadTimer;

    void Start()
    {
        bullets = maxBullets;
    }

    void Update()
    {
        if (bullets > 0)
        {
            Shot();
        }
        else
        {
            Reload();
        }
    }

    void Shot()
    {
        if (shotTimer >= timeToShot)
        {
            if (Input.GetButton("Fire1"))
            {
                Instantiate(bulletObject, bulletSpawn.position, transform.rotation);
            
                bullets -= 1;
                
                shotTimer = 0;
            }
        }
        else
        {
            shotTimer += Time.deltaTime;
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
}
