using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : MonoBehaviour
{
    public GameObject playerGunsWrapper;

    int gunsCount;
    GameObject[] playerGuns;

    void Start()
    {
        GetPlayerGunsObjects();
    }

    void Update()
    {
        
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

    public void SwitchCurrentGun(int newCurrentGunIndex)
    {
        for (int i = 0; i < gunsCount; i++)
        {
            bool isCurrentGunIndex = i == newCurrentGunIndex;

            playerGuns[i].SetActive(isCurrentGunIndex);
        }
    }
}
