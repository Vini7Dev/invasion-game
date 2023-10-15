using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContainer : MonoBehaviour
{
    public bool isPlayerAttack;
    public int maxDamage, minDamage;

    int projectileSpeed = 20;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    public void DefineProjectileProps(
        bool isPlayerAttack,
        int minDamage,
        int maxDamage,
        float projectDirection
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
                    maxDamage,
                    projectileSpeed * projectDirection
                );
            }
        }
    }
}
