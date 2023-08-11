using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMovement : ProjectileMovement
{
    Transform spriteTransform;
    Vector3 moveDirection;
    Quaternion reflectRotation;

    void Start()
    {
        projectileSpeed = 5;
        spriteTransform = transform;
        moveDirection = transform.forward;
    }

    void Update()
    {
        MoveByDirection();
        RotateAnimation();
    }

    void MoveByDirection()
    {
        transform.localPosition += moveDirection * projectileSpeed * Time.deltaTime;
    }

    void RotateAnimation()
    {
        spriteTransform.Rotate(Vector3.forward * 500 * Time.deltaTime);
    }

    void Bounce(Vector3 collisionNormal)
    {
        moveDirection = Vector3.Reflect(moveDirection, collisionNormal);
        reflectRotation = Quaternion.FromToRotation(moveDirection, collisionNormal);
        transform.rotation = reflectRotation * transform.rotation;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Scenary")
        {
            Bounce(collision.contacts[0].normal);
        }
        else
        {
            int damageToApply = Random.Range(minDamage, maxDamage + 1);

            if (isPlayerAttack && collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyController>().HaveHitADamage(damageToApply);
                collision.gameObject.GetComponent<EnemyMovement>().ApplyRepulsion();
            }
            else if (!isPlayerAttack && collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().HaveHitADamage(damageToApply);
			}
            else if (collision.gameObject.tag == "ItemBox")
            {
                collision.gameObject.GetComponent<ItemBox>().HaveHitADamage(damageToApply);
            }
        }
    }

    void OnTriggerEnter(Collider _) {}
}
