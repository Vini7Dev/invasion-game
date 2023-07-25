using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    bool friendlyBullet = false;
    int bulletSpeed = 45, minDamage = 10, maxDamage = 25;
    Vector3 forwardDirection = Vector3.forward;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void Update()
    {
        transform.Translate(forwardDirection * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Scenary")
        {
            Destroy(gameObject);
        }
        else
        {
            int damageToApply = Random.Range(minDamage, maxDamage + 1);

            Debug.Log("friendlyBullet: " + friendlyBullet + " damageToApply: " + damageToApply);

            if (friendlyBullet && other.tag == "Enemy")
            {
                Destroy(gameObject);   
                other.GetComponent<EnemyController>().HaveHitADamage(damageToApply);
            }
            else if (!friendlyBullet && other.tag == "Player")
            {
                Destroy(gameObject);  
                other.GetComponent<PlayerController>().HaveHitADamage(damageToApply);
            }
        }
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
}
