using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    protected bool isPlayerAttack = false;
    protected int minDamage = 10, maxDamage = 25;
    protected float projectileSpeed = 35;

    public void DefineProps(
        bool setIsPlayerAttack,
        int setMinDamage,
        int setMaxDamage
    )
    {
        isPlayerAttack = setIsPlayerAttack;
        minDamage = setMinDamage;
        maxDamage = setMaxDamage;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Scenary")
        {
            Destroy(gameObject);
        }
        else
        {
            int damageToApply = Random.Range(minDamage, maxDamage + 1);

            if (isPlayerAttack && other.tag == "Enemy")
            {
                Destroy(gameObject);
                other.GetComponent<EnemyController>().HaveHitADamage(damageToApply);
                other.GetComponent<EnemyMovement>().ApplyRepulsion();
            }
            else if (!isPlayerAttack && other.tag == "Player")
            {
                Destroy(gameObject);
                other.GetComponent<PlayerController>().HaveHitADamage(damageToApply);
			}
            else if (other.tag == "ItemBox")
            {
                Destroy(gameObject);
                other.GetComponent<ItemBox>().HaveHitADamage(damageToApply);
            }
        }
    }
}
