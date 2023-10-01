using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName
{
    NULL,
    Sword,
    Pistol,
    Shotgun
}

public enum SecondaryWeaponName
{
    NULL,
    Boomerang
}

public class GunsController : MonoBehaviour
{
    public Transform gunsWrapper;
    public Transform secondaryGunsWrapper;

    int gunsCount, secondaryGunsCount;
    GameObject[] gunObjects;
    GameObject[] secondaryGunObjects;

    protected FireGun currentFireGun;
    protected WhiteGun currentWhiteGun;
    protected SecondaryGun currentSecondaryFireGun;

    void Start()
    {
        GetGunObjects();
        GetSecondaryGunObjects();
        SwitchCurrentGun(WeaponName.Sword);
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

        secondaryGunsCount = secondaryGunsWrapper.childCount;

        secondaryGunObjects = new GameObject[secondaryGunsCount];

        for (int i = 0; i < secondaryGunsCount; i++)
        {
            Transform gunTransform = secondaryGunsWrapper.GetChild(i);

            if (gunTransform)
            {
                secondaryGunObjects[i] = gunTransform.gameObject;
            }
        }
    }

    public void SwitchCurrentGun(WeaponName gunName)
    {
        for (int i = 0; i < gunsCount; i++)
        {
            GameObject gunObject = gunObjects[i];

            if (!gunObject)
            {
                continue;
            }

            bool isGunToSetAsCurrent = gunObject.name == gunName.ToString();

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
        for (int i = 0; i < secondaryGunsCount; i++)
        {
            GameObject secondaryGunObject = secondaryGunObjects[i];

            bool isGunToSetAsCurrent = secondaryGunObject.name == gunName.ToString();

            if (!secondaryGunObject)
            {
                continue;
            }

            secondaryGunObject.SetActive(isGunToSetAsCurrent);

            if (!isGunToSetAsCurrent)
            {
                continue;
            }

            currentSecondaryFireGun = secondaryGunObject.GetComponent<SecondaryGun>();
        }
    }

    public string GetCurrentWihteGunName()
    {
        if (currentWhiteGun)
        {
            return currentWhiteGun.name;
        }

        return "";
    }

    public string GetCurrentFireGunName()
    {
        if (currentFireGun)
        {
            return currentFireGun.name;
        }

        return "";
    }

    public string GetCurrentSecondaryGunName()
    {
        if (currentSecondaryFireGun)
        {
            return currentSecondaryFireGun.name;
        }

        return "";
    }

    public void AddOneBulletOnSecondaryGun()
    {
        if (currentSecondaryFireGun)
        {
            currentSecondaryFireGun.AddOneBullet();
        }
    }
}
