using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteGun : Weapon
{
	float attackTimer, attackAnimationTimer, attackAnimationTime = 0.1f;

    void Update()
    {
        Attack();
        AttackAnimationTimer();
    }

    void Attack()
    {
        if (attackTimer >= attackTime)
        {
            if (Input.GetButton("Fire1"))
            {
                attackAnimationTimer = 0;
                attackTimer = 0;

				weaponAnimator.SetTrigger("Attack");
            }
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }

    void AttackAnimationTimer()
    {
        if (attackAnimationTimer < attackAnimationTime)
        {
            attackAnimationTimer += Time.deltaTime;
        }
    }

    bool AttackAnimationRunning()
    {
        return attackAnimationTimer < attackAnimationTime;
    }

	void OnTriggerEnter(Collider other) {
		if (!AttackAnimationRunning())
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
    }
}
