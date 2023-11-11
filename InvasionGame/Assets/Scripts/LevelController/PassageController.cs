using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageController : MonoBehaviour
{
    public GameObject passageConnection;

    RoomController roomController;
    CharacterController playerCharacterController;

    void Start()
    {
        GameObject roomObject = transform.parent.gameObject;
        roomController = roomObject.GetComponent<RoomController>();
    }

    public void CreatePassageConnection(GameObject passageToConnect)
    {
        SetPassageConnection(passageToConnect);

        passageToConnect.GetComponent<PassageController>().SetPassageConnection(gameObject);
    }

    public void SetPassageConnection(GameObject passageToConnect)
    {
        passageConnection = passageToConnect;
    }

    void TeleportPlayer(GameObject player)
    {
        if (!passageConnection) return;

        CharacterController playerCharacterController = player.GetComponent<CharacterController>();

        playerCharacterController.enabled = false;
        player.transform.position = passageConnection.transform.position;
        playerCharacterController.enabled = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Player" || !roomController.IsAllEnemiesDied())
        {
            return;
        }

        TeleportPlayer(other.gameObject);
    }
}
