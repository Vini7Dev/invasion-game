using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTouchDamage : DealTouchDamage
{
    protected override void ApplyDamage(Collider other)
    {
        EntitySkills entitySkills = other.GetComponent<EntitySkills>();

        if (entitySkills != null && entitySkills.canWalkInTrap) return;

        StartCoroutine(DamageDelay());

        EntityController entityController = other.GetComponent<EntityController>();
        entityController.HaveHitADamage(GetRandomDamage(), gameObject);
    }
}
