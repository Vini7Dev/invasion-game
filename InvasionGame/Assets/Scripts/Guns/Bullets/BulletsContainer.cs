using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsContainer : MonoBehaviour
{
    public bool isPlayerAttack;
    public int minDamage, maxDamage;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    public void DefineBulletsProps(
        bool isPlayerAttack,
        int minDamage,
        int maxDamage
    )
    {
        int bulletsCount = transform.childCount;

        for (int i = 0; i < bulletsCount; i++)
        {
            Transform bulletTransform = transform.GetChild(i);

            if (bulletTransform)
            {
                bulletTransform.GetComponent<BulletMovement>().DefineProps(
                    isPlayerAttack,
                    minDamage,
                    maxDamage
                );
            }            
        }
    }
}
