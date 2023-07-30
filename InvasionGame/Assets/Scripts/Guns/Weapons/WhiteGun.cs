using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteGun : Weapon
{
	float attackTimer;

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (attackTimer >= attackTime)
        {
            if (Input.GetButton("Fire1"))
            {
                attackTimer = 0f;

				weaponAnimator.SetTrigger("Attack");
            }
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }

	bool IsInAttackAnimation()
    {
        AnimatorStateInfo currentAnimationState = weaponAnimator.GetCurrentAnimatorStateInfo(0);

        return currentAnimationState.IsTag("Attack");
    }

	void OnTriggerStay(Collider other) {
		if (!IsInAttackAnimation())
		{
			return;
		}

		int damageToApply = Random.Range(minDamage, maxDamage + 1);

		if (isPlayerAttack && other.tag == "Enemy")
		{
			other.GetComponent<EnemyController>().HaveHitADamage(damageToApply);
		}
		else if (!isPlayerAttack && other.tag == "Player")
		{
			other.GetComponent<PlayerController>().HaveHitADamage(damageToApply);
		}
    }
}
