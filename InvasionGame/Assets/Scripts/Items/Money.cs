using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int moneyValue = 1;

    void CollectMoney()
    {
        GameObject levelControllerObject = GameObject.FindGameObjectWithTag(GlobalTags.LEVEL_CONTROLLER);

        LevelController levelController = levelControllerObject.GetComponent<LevelController>();

        levelController.IncrementMoneyValue(moneyValue);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != GlobalTags.PLAYER) return;

        CollectMoney();
    }
}
