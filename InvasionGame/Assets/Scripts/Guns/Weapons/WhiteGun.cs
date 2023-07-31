using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteGun : Weapon
{
    bool inDelayAttack, inAnimation;
	float attackAnimationTime = 0.1f;

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (inDelayAttack)
        {
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            if (!inAnimation)
            {
                weaponAnimator.SetTrigger("Attack");
            }

            StartCoroutine(AttackTime());
            StartCoroutine(AnimationTime());
        }
    }

	void OnTriggerEnter(Collider other) {
		if (!inAnimation)
		{
			return;
		}

		int damageToApply = Random.Range(minDamage, maxDamage + 1);

		if (isPlayerAttack && other.tag == "Enemy")
		{
			other.GetComponent<EnemyController>().HaveHitADamage(damageToApply);
            other.GetComponent<EnemyMovement>().ApplyRepulsion();
		}
		else if (!isPlayerAttack && other.tag == "Player")
		{
			other.GetComponent<PlayerController>().HaveHitADamage(damageToApply);
		}
        else if (other.tag == "ItemBox")
        {
            other.GetComponent<ItemBox>().HaveHitADamage(damageToApply * 100);
        }
    }

    IEnumerator AttackTime()
    {
        inDelayAttack = true;
        yield return new WaitForSeconds(attackTime);
        inDelayAttack = false;
    }

    IEnumerator AnimationTime()
    {
        inAnimation = true;
        yield return new WaitForSeconds(attackAnimationTime);
        inAnimation = false;
    }
}
