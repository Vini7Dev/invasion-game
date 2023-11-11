using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageController : MonoBehaviour
{
    public GameObject passageConnection;

    RoomController roomController;

    void Start()
    {
        GameObject roomObject = transform.parent.gameObject;
        roomController = roomObject.GetComponent<RoomController>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player" || !roomController.IsAllEnemiesDied())
        {
            return;
        }

        Debug.Log("HELLO WORLD!");
    }
}
