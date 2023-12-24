using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : Weapon
{
    const string SHOT_INPUT_BUTTON = "Fire1";

    public Transform bulletSpawnTransform;
    public GameObject projectileObject;
    public float triggerTime = 0.3f, reloadTime;
    public int maxBullets;

    int currentBullets;
    bool inDelayShot, inDelayReload;
    Transform fireGunsHandTransform;

    void Start()
    {
        base.Start();

        fireGunsHandTransform = transform.parent.parent;
        currentBullets = maxBullets;

        if (isPlayerAttack) UpdateAmmoInfoHUD();
    }

    void Update()
    {
        if (currentBullets <= 0 && !inDelayReload) StartCoroutine(ReloadTime());

        if (isPlayerAttack)
        {
            if (Input.GetButton(SHOT_INPUT_BUTTON))
            {
                Shot();
            }
        }
        else if (PlayerController.IsAlive())
        {
            Shot();
        }
    }

    float CalculeGunsRotationMultiplier()
    {
        return fireGunsHandTransform.localScale.x;
    }

    public void Shot()
    {
        if (inDelayShot || inDelayReload) return;

        StartCoroutine(ShotTime());

        GameObject projectileInstantiated = Instantiate(
            projectileObject,
            bulletSpawnTransform.position,
            transform.rotation
        );

        projectileInstantiated.GetComponent<ProjectileContainer>().DefineProjectileProps(
            isPlayerAttack,
            minDamage,
            maxDamage,
            CalculeGunsRotationMultiplier()
        );

        currentBullets -= 1;

        if (isPlayerAttack) UpdateAmmoInfoHUD();
    }

    void UpdateAmmoInfoHUD()
    {
        hudController.ammoInfo.UpdateAmmoInfo(currentBullets, maxBullets);
    }

    IEnumerator ShotTime()
    {
        inDelayShot = true;
        yield return new WaitForSeconds(triggerTime);
        inDelayShot = false;
    }

    IEnumerator ReloadTime()
    {
        inDelayReload = true;
        hudController.ammoInfo.ReloadingText();

        yield return new WaitForSeconds(reloadTime);

        currentBullets = maxBullets;
        UpdateAmmoInfoHUD();
        inDelayReload = false;
    }
}
