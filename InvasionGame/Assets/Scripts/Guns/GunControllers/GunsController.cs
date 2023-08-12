using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName
{
    Sword,
    Pistol,
    Shotgun
}

public enum SecondaryWeaponName
{
    Boomerang
}

public class GunsController : MonoBehaviour
{
    public Transform gunsWrapper;
    public Transform secondaryGunsWrapper;

    int gunsCount;
    GameObject[] gunObjects;
    GameObject[] secondaryGunObjects;

    protected FireGun currentFireGun;
    protected WhiteGun currentWhiteGun;
    protected FireGun currentSecondaryFireGun;

    void Start()
    {
        GetGunObjects();
        GetSecondaryGunObjects();
    }

    protected void GetGunObjects()
    {
        if (!gunsWrapper)
        {
            return;
        }

        gunsCount = gunsWrapper.childCount;

        gunObjects = new GameObject[gunsCount];

        for (int i = 0; i < gunsCount; i++)
        {
            Transform gunTransform = gunsWrapper.GetChild(i);

            if (gunTransform)
            {
                gunObjects[i] = gunTransform.gameObject;
            }
        }
    }

    protected void GetSecondaryGunObjects()
    {
        if (!secondaryGunsWrapper)
        {
            return;
        }

        gunsCount = secondaryGunsWrapper.childCount;

        gunObjects = new GameObject[gunsCount];

        for (int i = 0; i < gunsCount; i++)
        {
            Transform gunTransform = secondaryGunsWrapper.GetChild(i);

            if (gunTransform)
            {
                gunObjects[i] = gunTransform.gameObject;
            }
        }
    }

    public void SwitchCurrentGun(WeaponName gunName)
    {
        for (int i = 0; i < gunsCount; i++)
        {
            GameObject gunObject = gunObjects[i];

            bool isGunToSetAsCurrent = gunObject.name == gunName.ToString();

            if (!gunObject)
            {
                continue;
            }

            gunObject.SetActive(isGunToSetAsCurrent);

            if (!isGunToSetAsCurrent)
            {
                continue;
            }

            if (gunObject.GetComponent<Weapon>().IsFiregun())
            {
                currentFireGun = gunObject.GetComponent<FireGun>();
            }
            else
            {
                currentWhiteGun = gunObject.GetComponent<WhiteGun>();
            }
        }
    }

    public void SwitchSecondaryGun(SecondaryWeaponName gunName)
    {
        for (int i = 0; i < gunsCount; i++)
        {
            GameObject gunObject = secondaryGunObjects[i];

            bool isGunToSetAsCurrent = gunObject.name == gunName.ToString();

            if (!gunObject)
            {
                continue;
            }

            gunObject.SetActive(isGunToSetAsCurrent);

            if (!isGunToSetAsCurrent)
            {
                continue;
            }

            currentSecondaryFireGun = gunObject.GetComponent<FireGun>();
        }
    }
}
