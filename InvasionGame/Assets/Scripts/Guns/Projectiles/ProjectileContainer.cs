using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContainer : MonoBehaviour
{
    public bool disableAutoDestroy, isPlayerAttack;
    public int minDamage, maxDamage;

    void Start()
    {
        if (!disableAutoDestroy)
        {
            Destroy(gameObject, 3);
        }
    }

    public void DefineProjectileProps(
        bool isPlayerAttack,
        int minDamage,
        int maxDamage
    )
    {
        int projectilesCount = transform.childCount;

        for (int i = 0; i < projectilesCount; i++)
        {
            Transform projectileTransform = transform.GetChild(i);

            if (projectileTransform)
            {
                projectileTransform.GetComponent<ProjectileMovement>().DefineProps(
                    isPlayerAttack,
                    minDamage,
                    maxDamage
                );
            }            
        }
    }
}
