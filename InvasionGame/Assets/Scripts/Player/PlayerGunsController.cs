using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunsController : MonoBehaviour
{
    public GameObject playerGunsWrapper;

    int gunsCount;
    GameObject[] playerGuns;

    void Start()
    {
        GetPlayerGunsObjects();
    }

    void GetPlayerGunsObjects()
    {
        gunsCount = transform.childCount;

        playerGuns = new GameObject[gunsCount];

        for (int i = 0; i < gunsCount; i++)
        {
            playerGuns[i] = playerGunsWrapper.transform.GetChild(i).gameObject;
        }
    }

    void SwitchCurrentGun(int newCurrentGunIndex)
    {
        for (int i = 0; i < gunsCount; i++)
        {
            bool isCurrentGunIndex = i == newCurrentGunIndex;

            playerGuns[i].SetActive(isCurrentGunIndex);
        }
    }
}
