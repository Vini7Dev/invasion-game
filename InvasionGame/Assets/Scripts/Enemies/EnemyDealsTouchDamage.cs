using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealsTouchDamage : MonoBehaviour
{
    float damageDalay = 0.2f, damageDelayTimer = 0;

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player")
        {

        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            damageDelayTimer = 0;
        }
    }
}
