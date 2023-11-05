using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableSceneryController : EntityController
{
    protected override IEnumerator DamageTimer()
    {
        onDamage = true;

        yield return new WaitForSeconds(damageTime);

        onDamage = false;
    }
}
