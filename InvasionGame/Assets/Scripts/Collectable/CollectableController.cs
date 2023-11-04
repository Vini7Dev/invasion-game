using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    const string COLLECTABLE_TAG = "Collectable";

    ItemType currentWeaponItemType = ItemType.WhiteGun;

    EntityWhiteGunHands entityWhiteGunHands;
    EntityFireGunHands entityFireGunHands;
    GameObject whiteGunLeftHand, fireGunLeftHand, fireGunRightHand;

    void Start()
    {
        entityWhiteGunHands = GetComponent<EntityWhiteGunHands>();
        entityFireGunHands = GetComponent<EntityFireGunHands>();

        whiteGunLeftHand = entityWhiteGunHands.whiteGunLeftHand;
        fireGunLeftHand = entityFireGunHands.fireGunLeftHand;
        fireGunRightHand = entityFireGunHands.fireGunRightHand;

        ShowOrHideHands();
    }

    void ShowOrHideHands()
    {
        bool isWhiteGun = currentWeaponItemType == ItemType.WhiteGun;

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
        float zPositionRelative
    )
    {
        GameObject objectInstance = Instantiate(objectToInstantiate, handTransform);

        if (zPositionRelative == 0) return;

        Vector3 objectPosition = new Vector3(0, 0, zPositionRelative);

        objectInstance.transform.localPosition = objectPosition;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != COLLECTABLE_TAG) return;

        CollectableItem collectableItem = other.GetComponent<CollectableItem>();

        if (collectableItem.itemType == ItemType.Item) return;

        currentWeaponItemType = collectableItem.itemType;

        RemoveAllHandChildren(whiteGunLeftHand.transform);
        RemoveAllHandChildren(fireGunLeftHand.transform);
        RemoveAllHandChildren(fireGunRightHand.transform);

        ShowOrHideHands();

        if (currentWeaponItemType == ItemType.WhiteGun)
        {
            InstantiateWeaponOnHand(
                whiteGunLeftHand.transform,
                collectableItem.collectableObject,
                collectableItem.zPositionRelative
            );
        }
        else
        {
            InstantiateWeaponOnHand(
                fireGunLeftHand.transform,
                collectableItem.collectableObject,
                collectableItem.zPositionRelative
            );
            InstantiateWeaponOnHand(
                fireGunRightHand.transform,
                collectableItem.collectableObject,
                collectableItem.zPositionRelative
            );
        }

        Destroy(other.gameObject);
    }
}
