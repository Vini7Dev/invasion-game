using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : MonoBehaviour
{
    public Transform gunsWrapper;

    int gunsCount;
    GameObject[] gunObjects;
    protected FireGun currentFireGun;

    void Start()
    {
        GetGunObjects();
    }

    protected void GetGunObjects()
    {
        gunsCount = transform.childCount;

        gunObjects = new GameObject[gunsCount];

        for (int i = 0; i < gunsCount; i++)
        {
            Transform gunTransform = gunsWrapper.GetChild(i);

            if (!gunTransform)
            {
                continue;
            }

            gunObjects[i] = gunTransform.gameObject;
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

            if (isCurrentGunIndex)
            {
                currentFireGun = gunObject.GetComponent<FireGun>();
            }
        }
    }
}
