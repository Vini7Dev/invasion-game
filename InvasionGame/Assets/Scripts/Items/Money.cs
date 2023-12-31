using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    const string LEVEL_CONTROLLER_TAG = "GameController";
    const string PLAYER_TAG = "Player";

    public int moneyValue = 1;

    void CollectMoney()
    {
        GameObject levelControllerObject = GameObject.FindGameObjectWithTag(LEVEL_CONTROLLER_TAG);

        LevelController levelController = levelControllerObject.GetComponent<LevelController>();

        levelController.IncrementMoneyValue(moneyValue);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != PLAYER_TAG) return;

        CollectMoney();
    }
}
