using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    protected const string ENEMY_TAG = "Enemy";
    protected const string PLAYER_TAG = "Player";
    protected const string SCENARY_TAG = "Scenary";

    bool isPlayerAttack = false;
    float projectileSpeed;
    int maxDamage, minDamage;
    Vector3 forwardDirection = Vector3.right;

    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(forwardDirection * projectileSpeed * Time.deltaTime);
    }

    public void DefineProps(
        bool setIsPlayerAttack,
        int setMinDamage,
        int setMaxDamage,
        float setProjectileSpeed
    )
    {
        isPlayerAttack = setIsPlayerAttack;
        minDamage = setMinDamage;
        maxDamage = setMaxDamage;
        projectileSpeed = setProjectileSpeed;

        if (setProjectileSpeed < 0)
        {
            Vector3 invertedScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z
            );

            transform.localScale = invertedScale;
        }
    }

    int GetRandomDamage() {
        return Random.Range(minDamage, maxDamage + 1);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == SCENARY_TAG)
        {
            Destroy(gameObject);
        }
        else
        {
            int damageToApply = GetRandomDamage();

            EntityController entityController = other.GetComponent<EntityController>();

            if (isPlayerAttack && other.tag == ENEMY_TAG)
            {
                entityController.HaveHitADamage(damageToApply);
                Destroy(gameObject);
            }
            else if (!isPlayerAttack && other.tag == PLAYER_TAG)
            {
                entityController.HaveHitADamage(damageToApply);
                Destroy(gameObject);
			}
        }
    }
}
