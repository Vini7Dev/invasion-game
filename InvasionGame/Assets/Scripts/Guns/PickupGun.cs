using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGun : MonoBehaviour
{
    public WeaponName weaponName = WeaponName.Sword;

    bool availableToCollect = false;
    float timeToMakeAvailable = 0.5f;
    GameObject player;

    void Update()
    {
        if (!availableToCollect && timeToMakeAvailable > 0)
        {
            timeToMakeAvailable -= Time.deltaTime;
        }
        else
        {
            availableToCollect = true;
        }
    }

    void CollectGun()
    {
        if (!availableToCollect)
        {
            return;
        }

        player.GetComponent<GunsController>().SwitchCurrentGun(weaponName);

        Destroy(gameObject);
    }

    void OnTriggerStay(Collider other) {
        if (other.tag != "Player")
        {
            return;
        }

        player = other.gameObject;
        CollectGun();
    }
}
