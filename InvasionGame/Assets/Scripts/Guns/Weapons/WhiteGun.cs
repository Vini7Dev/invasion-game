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

    IEnumerator ApplyRepulsion(EnemyMovement enemyMovement)
    {
        enemyMovement.walkSpeed = -Mathf.Abs(enemyMovement.walkSpeed);

        yield return new WaitForSeconds(0.2f);

        enemyMovement.walkSpeed = Mathf.Abs(enemyMovement.walkSpeed);
    }

    IEnumerator ApplyRepulsion(PlayerMovement playerMovement)
    {
        playerMovement.walkSpeed = -Mathf.Abs(playerMovement.walkSpeed);

        yield return new WaitForSeconds(0.2f);

        playerMovement.walkSpeed = Mathf.Abs(playerMovement.walkSpeed);
    }

	void OnTriggerEnter(Collider other) {
        Debug.Log(AttackAnimationRunning());

		if (!AttackAnimationRunning())
		{
			return;
		}

		int damageToApply = Random.Range(minDamage, maxDamage + 1);

		if (isPlayerAttack && other.tag == "Enemy")
		{
			other.GetComponent<EnemyController>().HaveHitADamage(damageToApply);
            EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
			StartCoroutine(ApplyRepulsion(enemyMovement));
		}
		else if (!isPlayerAttack && other.tag == "Player")
		{
			other.GetComponent<PlayerController>().HaveHitADamage(damageToApply);
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
			StartCoroutine(ApplyRepulsion(playerMovement));
		}
    }
}
