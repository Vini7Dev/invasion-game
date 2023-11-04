using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
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
    }
}
