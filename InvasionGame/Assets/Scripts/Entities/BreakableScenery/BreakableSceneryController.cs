using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class DropItemConfig
{
    public GameObject itemObject;
    public int probability;
}

public class BreakableSceneryController : EntityController
{
    public DropItemConfig[] itemsConfig;

    void DropRandomItem()
    {
        for (int i = 0; i < itemsConfig.Length; i++)
        {
            int dropChance = Random.Range(0, 101);

            if (dropChance <= itemsConfig[i].probability)
            {
                GameObject instanciatedItem = Instantiate(
                    itemsConfig[i].itemObject,
                    transform.position,
                    itemsConfig[i].itemObject.transform.rotation
                );

                instanciatedItem.transform.parent = transform.parent;

                break;
            }
        }
    }

    protected override void WhenDying()
    {
        DropRandomItem();

        Destroy(gameObject);
    }

    protected override void YPositionCorrection() {}

    protected override IEnumerator DamageTimer()
    {
        onDamage = true;

        yield return new WaitForSeconds(damageTime);

        onDamage = false;
    }
}
