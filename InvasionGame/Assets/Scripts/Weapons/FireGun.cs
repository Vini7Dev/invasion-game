using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : Weapon
{
    const string SHOT_INPUT_BUTTON = "Fire1";

    public Transform bulletSpawnTransform;
    public GameObject projectileObject;
    public float triggerTime = 0.3f;

    bool inDelayShot;
    Transform fireGunsHandTransform;

    void Start()
    {
        fireGunsHandTransform = transform.parent.parent;
    }

    void Update()
    {
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
        if (inDelayShot)
        {
            return;
        }

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
    }

    IEnumerator ShotTime()
    {
        inDelayShot = true;
        yield return new WaitForSeconds(triggerTime);
        inDelayShot = false;
    }
}
