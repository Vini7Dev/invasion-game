using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : Weapon
{
    public AudioClip shotSound, reloadSound;
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
        if (currentBullets <= 0) Reload();

        if (isPlayerAttack)
        {
            if (Input.GetButton(GlobalButtons.SHOT)) Shot();

            if (
                Input.GetButtonDown(GlobalButtons.RELOAD) && currentBullets < maxBullets
            ) Reload();
        }
        else if (PlayerController.IsAlive())
        {
            if (currentBullets <= 0) Reload();
            else Shot();
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

        PlaySound(shotSound);

        currentBullets -= 1;

        if (isPlayerAttack) UpdateAmmoInfoHUD();
    }

    void Reload()
    {
        if (!inDelayReload) StartCoroutine(ReloadTime());
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

        if (isPlayerAttack)
        {
            PlaySound(reloadSound, reloadTime);
            hudController.ammoInfo.ReloadingText();
        }

        yield return new WaitForSeconds(reloadTime);

        currentBullets = maxBullets;
        if (isPlayerAttack) UpdateAmmoInfoHUD();
        inDelayReload = false;
    }
}
