using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
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

    int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    void ApplyDamage(Collider other)
    {
        int damageToApply = GetRandomDamage();

        EntityController entityController = other.GetComponent<EntityController>();

        entityController.HaveHitADamage(damageToApply, gameObject);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == GlobalTags.OUT_OF_WALL)
        {
            Destroy(gameObject);
        }
        else
        {
            if (isPlayerAttack && other.tag == GlobalTags.ENEMY) ApplyDamage(other);
            else if (!isPlayerAttack && other.tag == GlobalTags.PLAYER) ApplyDamage(other);
            else if (other.tag == GlobalTags.BREAKABLE_SCENERY) ApplyDamage(other);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != GlobalTags.INTERACTIVE_OBJECT) return;

        float distance = Vector3.Distance(transform.position, other.transform.position);

        if (distance < 0.5f) Destroy(gameObject);
    }
}
