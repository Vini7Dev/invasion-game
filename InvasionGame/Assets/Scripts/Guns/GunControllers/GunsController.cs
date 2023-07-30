using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : MonoBehaviour
{
    public Transform gunsWrapper;

    int gunsCount;
    GameObject[] gunObjects;

    protected FireGun currentFireGun;
    protected WhiteGun currentWhiteGun;

    void Start()
    {
        GetGunObjects();
    }

    protected void Update()
    {
        FlipGunSpriteByRotation();
    }

    void FlipGunSpriteByRotation()
    {
        if (currentFireGun || currentWhiteGun) {
            SpriteRenderer currentGunSprite = currentFireGun.GetComponent<SpriteRenderer>();

            currentGunSprite.flipY = gunsWrapper.rotation.y < 0;
        }
    }

    protected void GetGunObjects()
    {
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

    public void SwitchCurrentGun(string gunName)
    {
        for (int i = 0; i < gunsCount; i++)
        {
            GameObject gunObject = gunObjects[i];

            bool isGunToSetAsCurrent = gunObject.name == gunName;

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
}
