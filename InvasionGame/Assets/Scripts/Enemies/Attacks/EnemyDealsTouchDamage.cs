using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealsTouchDamage : MonoBehaviour
{

    public int minDamage, maxDamage;
    
    float damageDalay = 0.5f, damageDelayTimer;

    void Start()
    {
        damageDelayTimer = damageDalay;
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player")
        {
            if (damageDelayTimer < damageDalay)
            {
                damageDelayTimer += Time.deltaTime;
            }
            else
            {
                damageDelayTimer = 0;

                int damageToApply = Random.Range(minDamage, maxDamage + 1);
                
                PlayerController playerController = other.GetComponent<PlayerController>();

                playerController.HaveHitADamage(damageToApply);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            damageDelayTimer = damageDalay;
        }
    }
}
