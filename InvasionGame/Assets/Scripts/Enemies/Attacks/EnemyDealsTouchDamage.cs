using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealsTouchDamage : MonoBehaviour
{
    public int minDamage, maxDamage;
    
    float damageDalay = 0.5f;
    bool causedDamage;

    void OnTriggerStay(Collider other) {
        if (other.tag != "Player" || causedDamage)
        {
            return;
        }

        StartCoroutine(DamageDelay());

        int damageToApply = Random.Range(minDamage, maxDamage + 1);
        other.GetComponent<PlayerController>().HaveHitADamage(damageToApply);
    }

    IEnumerator DamageDelay()
    {
        causedDamage = true;
        yield return new WaitForSeconds(damageDalay);  
        causedDamage = false;
    }
}
