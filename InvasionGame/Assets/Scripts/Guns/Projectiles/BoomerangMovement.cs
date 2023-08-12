using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMovement : ProjectileMovement
{
    public Transform boomerangTransform;

    Vector3 moveDirection;
    Quaternion reflectRotation;

    void Start()
    {
        projectileSpeed = 14;
        PointToMouse();
    }

    void Update()
    {
        MoveByDirection();
        RotateAnimation();
    }

    void PointToMouse()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = Camera.main.transform.position.y;

        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(
            mousePositionScreen
        );
        Vector3 mousePositionIn2D = new Vector3(
            mousePositionWorld.x,
            0,
            mousePositionWorld.z
        );
        Vector3 selfPositionWithoutY = new Vector3(
            transform.position.x,
            0,
            transform.position.z
        );

        moveDirection = (mousePositionIn2D - selfPositionWithoutY).normalized;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void MoveByDirection()
    {
        transform.position += moveDirection * projectileSpeed * Time.deltaTime;
    }

    void RotateAnimation()
    {
        boomerangTransform.Rotate(Vector3.forward * 500 * Time.deltaTime);
    }

    void Bounce(Vector3 collisionNormal)
    {
        moveDirection = Vector3.Reflect(moveDirection, collisionNormal);
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
