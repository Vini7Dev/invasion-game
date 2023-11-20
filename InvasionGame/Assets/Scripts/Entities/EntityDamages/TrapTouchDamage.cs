using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTouchDamage : DealTouchDamage
{
    public GameObject trapDamageObject;
    public float timeToActiveTrap = 1;

    bool isActive, inTimer;

    void Update()
    {
        if (!inTimer) {
            StartCoroutine(ToggleTrapAtiveTimer());
        }
    }

    IEnumerator ToggleTrapAtiveTimer()
    {
        inTimer = true;

        yield return new WaitForSeconds(timeToActiveTrap);

        isActive = !isActive;
        inTimer = false;

        if (trapDamageObject) {
            trapDamageObject.SetActive(isActive);
        }
    }

    protected override void ApplyDamage(Collider other)
    {
        if (!isActive) return;

        EntitySkills entitySkills = other.GetComponent<EntitySkills>();

        if (entitySkills != null && entitySkills.canWalkInTrap) return;

        StartCoroutine(DamageDelay());

        EntityController entityController = other.GetComponent<EntityController>();
        entityController.HaveHitADamage(GetRandomDamage(), gameObject);
    }
}
