using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTouchDamage : DealTouchDamage
{
    public GameObject trapDamageObject;
    public Animator trapAnimator;
    public float timeToActiveTrap = 1;

    bool isActive, inTimer;

    void OnEnable()
    {
        inTimer = false;
        isActive = false;
        ToggleTrapDamageObject(isActive);
    }

    void Update()
    {
        if (!inTimer)
        {
            StartCoroutine(ToggleTrapAtiveTimer());
        }
    }

    IEnumerator ToggleTrapAtiveTimer()
    {
        inTimer = true;

        yield return new WaitForSeconds(timeToActiveTrap);

        isActive = !isActive;
        inTimer = false;

        ToggleTrapDamageObject(isActive);
    }

    void ToggleTrapDamageObject(bool setIsActive)
    {
        if (trapAnimator)
        {
            trapAnimator.SetBool("Enabled", !setIsActive);
        }
        else if (trapDamageObject)
        {
            trapDamageObject.SetActive(setIsActive);
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
