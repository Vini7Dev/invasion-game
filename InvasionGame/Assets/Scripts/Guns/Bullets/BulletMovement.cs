using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public GameObject damageTextParticleObject;

    bool friendlyBullet = false;
    int bulletSpeed = 35, minDamage = 10, maxDamage = 25;
    Vector3 forwardDirection = Vector3.right;

    void Update()
    {
        transform.Translate(forwardDirection * bulletSpeed * Time.deltaTime);
    }

    public void DefineProps(
        bool setFriendlyBullet,
        int setMinDamage,
        int setMaxDamage
    )
    {
        friendlyBullet = setFriendlyBullet;
        minDamage = setMinDamage;
        maxDamage = setMaxDamage;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Scenary")
        {
            Destroy(gameObject);
        }
        else
        {
            int damageToApply = Random.Range(minDamage, maxDamage + 1);

            if (friendlyBullet && other.tag == "Enemy")
            {
                Destroy(gameObject);   
                other.GetComponent<EnemyController>().HaveHitADamage(damageToApply);

                CreateDamageTextParticle(damageToApply, 0);
            }
            else if (!friendlyBullet && other.tag == "Player")
            {
                Destroy(gameObject);  
                other.GetComponent<PlayerController>().HaveHitADamage(damageToApply);

                CreateDamageTextParticle(damageToApply, 1);
            }
        }
    }

    void CreateDamageTextParticle(
        int damageValue,
        int damageColorIndex
    ) {
        Quaternion damageTextParticleRotation = Quaternion.Euler(
            new Vector3(90, 0, 0)
        );

        GameObject damageTextParticle = Instantiate(
            damageTextParticleObject,
            transform.position,
            damageTextParticleRotation
        );

        damageTextParticle.GetComponent<DamageTextParticle>().SetupDamageText(
            damageValue,
            damageColorIndex
        );
    }
}
