using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    protected const string ENEMY_TAG = "Enemy";
    protected const string PLAYER_TAG = "Player";
    protected const string BREAKABLE_SCENERY_TAG = "BreakableScenery";
    protected const string OUT_OF_WALL_TAG = "OutOfWall";
    protected const string INTERACTIVE_OBJECT_TAG = "InteractiveObject";

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
        if (other.tag == OUT_OF_WALL_TAG || other.tag == INTERACTIVE_OBJECT_TAG)
        {
            Destroy(gameObject);
        }
        else
        {
            if (isPlayerAttack && other.tag == ENEMY_TAG) ApplyDamage(other);
            else if (!isPlayerAttack && other.tag == PLAYER_TAG) ApplyDamage(other);
            else if (other.tag == BREAKABLE_SCENERY_TAG) ApplyDamage(other);
        }
    }
}
