using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public WeaponName weaponName = WeaponName.NULL;
    public SecondaryWeaponName secondaryWeaponName = SecondaryWeaponName.NULL;

    bool availableToCollect = false;
    float timeToMakeAvailable = 0.5f;
    GameObject player;

    void Start()
    {
        StartCoroutine(AvailableToCollectTime());

        transform.position = new Vector3(
            transform.position.x,
            0.14f,
            transform.position.z
        );
    }

    void CollectGun()
    {
        if (!availableToCollect)
        {
            return;
        }

        if (weaponName != WeaponName.NULL)
        {
            player.GetComponent<GunsController>().SwitchCurrentGun(weaponName);
        }
        else
        {
            player.GetComponent<GunsController>().SwitchSecondaryGun(secondaryWeaponName);
        }

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

    IEnumerator AvailableToCollectTime()
    {
        availableToCollect = false;
        yield return new WaitForSeconds(timeToMakeAvailable);
        availableToCollect = true;
    }
}
