using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMovement : ProjectileMovement
{
    public Transform boomerangTransform;

    Vector3 moveDirection;
    Quaternion reflectRotation;
    bool availableToCatch;

    void Start()
    {
        projectileSpeed = 20;
        PointToMouse();
        StartCoroutine(SetAvailableToCatch());
    }

    void Update()
    {
        MoveByDirection();
        RotateAnimation();
    }

    bool OnAttack()
    {
        return projectileSpeed > 0;
    }

    void PointToMouse()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = Camera.main.transform.position.y;

        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        Vector3 mousePositionIn2D = new Vector3(mousePositionWorld.x, 0, mousePositionWorld.z);
        Vector3 selfPositionWithoutY = new Vector3(transform.position.x, 0, transform.position.z);

        moveDirection = (mousePositionIn2D - selfPositionWithoutY).normalized;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void MoveByDirection()
    {
        transform.position += moveDirection * projectileSpeed * Time.deltaTime;

        if (OnAttack())
        {
            projectileSpeed -= Time.deltaTime * 3f;
        }
        else
        {
            projectileSpeed = 0;
        }
    }

    void RotateAnimation()
    {
        boomerangTransform.Rotate(
            projectileSpeed * 50 * Vector3.forward * Time.deltaTime
        );
    }

    void Bounce(Vector3 collisionNormal)
    {
        moveDirection = Vector3.Reflect(moveDirection, collisionNormal);
    }

    void OnCollideInScenary(Collision scenaryCollision)
    {
        if (!OnAttack())
        {
            return;
        }

        Bounce(scenaryCollision.contacts[0].normal);
    }

    void OnCollideInPlayer(GameObject player)
    {
        if (!isPlayerAttack)
        {
            if (!OnAttack())
            {
                return;
            }

            int damageToApply = Random.Range(minDamage, maxDamage + 1);
            player.GetComponent<PlayerController>().HaveHitADamage(damageToApply);
        } else if (availableToCatch) {
            PlayerGunsController playerGunsController = player.GetComponent<PlayerGunsController>();

            string secondaryGunName = playerGunsController.GetCurrentSecondaryGunName();

            if (secondaryGunName.ToString() == "Boomerang")
            {
                playerGunsController.AddOneBulletOnSecondaryGun();
            }
            else
            {
                playerGunsController.SwitchSecondaryGun(SecondaryWeaponName.Boomerang);
            }
            
            Destroy(gameObject);
        }
    }

    void OnCollideInEnemy(GameObject enemy)
    {
        if (isPlayerAttack)
        {
            if (!OnAttack())
            {
                return;
            }

            int damageToApply = Random.Range(minDamage, maxDamage + 1);
            enemy.GetComponent<EnemyController>().HaveHitADamage(damageToApply);
            enemy.GetComponent<EnemyMovement>().ApplyRepulsion();
        }
    }

    void OnCollideInItemBox(GameObject itemBox)
    {
        if (!OnAttack())
        {
            return;
        }

        int damageToApply = Random.Range(minDamage, maxDamage + 1);
        itemBox.GetComponent<ItemBox>().HaveHitADamage(damageToApply);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Scenary")
        {
            OnCollideInScenary(collision);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy")
        {
            OnCollideInEnemy(other.gameObject);
        }
        else if (other.tag == "Player")
        {
            OnCollideInPlayer(other.gameObject);
        }
        else if (other.tag == "ItemBox")
        {
            OnCollideInItemBox(other.gameObject);
        }
    }

    IEnumerator SetAvailableToCatch()
    {
        yield return new WaitForSeconds(1);  
        availableToCatch = true;
    }
}
