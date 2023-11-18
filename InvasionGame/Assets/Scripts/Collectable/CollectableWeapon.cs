using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    FireGun,
    WhiteGun
}

public class CollectableWeapon : MonoBehaviour
{
    public WeaponType weaponType = WeaponType.FireGun;
    public GameObject weapon;
    public float zPositionRelative = 0;
}
