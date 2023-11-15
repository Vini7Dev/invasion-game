using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealTouchDamage : MonoBehaviour
{
    public string[] tagsToCauseDamage;
    public int maxDamage = 10, minDamage = 5;

    float damageDalay = 0.5f;
    bool causedDamage;

    protected int GetRandomDamage() {
        return Random.Range(minDamage, maxDamage + 1);
    }

    bool IsInTagsArray(string tagToCheck) {
        return System.Array.Exists(
            tagsToCauseDamage,
            element => element == tagToCheck
        );
    }

    protected virtual void ApplyDamage(Collider other)
    {
        StartCoroutine(DamageDelay());

        EntityController entityController = other.GetComponent<EntityController>();
        entityController.HaveHitADamage(GetRandomDamage(), gameObject);
    }

    void OnTriggerStay(Collider other) {
        if (!IsInTagsArray(other.tag) || causedDamage)
        {
            return;
        }

        ApplyDamage(other);
    }

    protected IEnumerator DamageDelay()
    {
        causedDamage = true;
        yield return new WaitForSeconds(damageDalay);
        causedDamage = false;
    }
}
