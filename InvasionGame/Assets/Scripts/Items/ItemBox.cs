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

public class ItemBox : MonoBehaviour
{
    public int boxLife = 30;
    public DropItemConfig[] itemsConfig;

    void DropRandomItem()
    {
        for (int i = 0; i < itemsConfig.Length; i++)
        {
            int dropChance = Random.Range(0, 101);

            if (dropChance <= itemsConfig[i].probability)
            {
                Instantiate(
                    itemsConfig[i].itemObject, 
                    transform.position,
                    itemsConfig[i].itemObject.transform.rotation
                );
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);
    }

    public void HaveHitADamage(int damageReceived)
    {
        boxLife -= damageReceived;

        if (boxLife <= 0)
        {
            DropRandomItem();
        }
    }
}
