using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFireGunHands : MonoBehaviour
{
    public GameObject fireGunHands;
    public GameObject fireGunLeftHand;
    public GameObject fireGunRightHand;

    protected EntitySkills entitySkills;

    protected void Start()
    {
        entitySkills = GetComponent<EntitySkills>();
    }
}
