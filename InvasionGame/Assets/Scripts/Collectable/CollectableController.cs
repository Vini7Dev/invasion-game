using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    const string COLLECTABLE_WEAPON_TAG = "CollectableWeapon";
    const string COLLECTABLE_ITEM_TAG = "CollectableItem";

    WeaponType currentWeaponType = WeaponType.WhiteGun;

    EntityController entityController;
    EntityWhiteGunHands entityWhiteGunHands;
    EntityFireGunHands entityFireGunHands;
    GameObject whiteGunLeftHand, fireGunLeftHand, fireGunRightHand;

    void Start()
    {
        entityController = GetComponent<EntityController>();
        entityWhiteGunHands = GetComponent<EntityWhiteGunHands>();
        entityFireGunHands = GetComponent<EntityFireGunHands>();

        whiteGunLeftHand = entityWhiteGunHands.whiteGunLeftHand;
        fireGunLeftHand = entityFireGunHands.fireGunLeftHand;
        fireGunRightHand = entityFireGunHands.fireGunRightHand;

        ShowOrHideHands();
    }

    void ShowOrHideHands()
    {
        bool isWhiteGun = currentWeaponType == WeaponType.WhiteGun;

        entityWhiteGunHands.enabled = isWhiteGun;
        whiteGunLeftHand.SetActive(isWhiteGun);

        entityFireGunHands.enabled = !isWhiteGun;
        fireGunLeftHand.SetActive(!isWhiteGun);
        fireGunRightHand.SetActive(!isWhiteGun);
    }

    void RemoveAllHandChildren(Transform handTransform)
    {
        foreach (Transform child in handTransform)
        {
            Destroy(child.gameObject);
        }
    }

    void InstantiateWeaponOnHand(
        Transform handTransform,
        GameObject objectToInstantiate,
        float zPositionRelative = 0
    )
    {
        GameObject objectInstance = Instantiate(objectToInstantiate, handTransform);

        Vector3 objectPosition = new Vector3(0, 0, zPositionRelative);

        objectInstance.transform.localPosition = objectPosition;
    }

    void CollectWeapon(Collider other)
    {
        CollectableWeapon collectableWeapon = other.GetComponent<CollectableWeapon>();

        currentWeaponType = collectableWeapon.weaponType;

        RemoveAllHandChildren(whiteGunLeftHand.transform);
        RemoveAllHandChildren(fireGunLeftHand.transform);
        RemoveAllHandChildren(fireGunRightHand.transform);

        ShowOrHideHands();

        if (currentWeaponType == WeaponType.WhiteGun)
        {
            InstantiateWeaponOnHand(
                whiteGunLeftHand.transform,
                collectableWeapon.weapon,
                collectableWeapon.zPositionRelative
            );
        }
        else
        {
            InstantiateWeaponOnHand(
                fireGunLeftHand.transform,
                collectableWeapon.weapon,
                collectableWeapon.zPositionRelative
            );
            InstantiateWeaponOnHand(
                fireGunRightHand.transform,
                collectableWeapon.weapon,
                collectableWeapon.zPositionRelative
            );
        }

        Destroy(other.gameObject);
    }

    void CollectItem(Collider other)
    {
        CollectableItem collectableItem = other.GetComponent<CollectableItem>();

        if (collectableItem.itemType == ItemType.Health)
        {
            entityController.AddLife(collectableItem.buffValue);
        }

        Destroy(other.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == COLLECTABLE_WEAPON_TAG) CollectWeapon(other);
        else if (other.tag == COLLECTABLE_ITEM_TAG) CollectItem(other);
    }
}
