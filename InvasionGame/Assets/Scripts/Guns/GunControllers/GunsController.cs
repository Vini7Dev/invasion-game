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

    public void SwitchCurrentGun(int newCurrentGunIndex)
    {
        for (int i = 0; i < gunsCount; i++)
        {
            bool isCurrentGunIndex = i == newCurrentGunIndex;

            GameObject gunObject = gunObjects[i];

            if (!gunObject)
            {
                continue;
            }

            gunObject.SetActive(isCurrentGunIndex);

            if (!isCurrentGunIndex)
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
