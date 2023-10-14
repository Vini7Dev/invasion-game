using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealTouchDamage : MonoBehaviour
{
    public string[] tagsToCauseDamage;
    public int maxDamage = 10, minDamage = 5;

    float damageDalay = 0.5f;
    bool causedDamage;

    int GetRandomDamage() {
        return Random.Range(minDamage, maxDamage + 1);
    }

    bool IsInTagsArray(string tagToCheck) {
        return System.Array.Exists(
            tagsToCauseDamage,
            element => element == tagToCheck
        );
    }

    void OnTriggerStay(Collider other) {
        if (!IsInTagsArray(other.tag) || causedDamage)
        {
            return;
        }

        StartCoroutine(DamageDelay());

        EntityController entityController = other.GetComponent<EntityController>();
        entityController.HaveHitADamage(GetRandomDamage());
    }

    IEnumerator DamageDelay()
    {
        causedDamage = true;
        yield return new WaitForSeconds(damageDalay);
        causedDamage = false;
    }
}
