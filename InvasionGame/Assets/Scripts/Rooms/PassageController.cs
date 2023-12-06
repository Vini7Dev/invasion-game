using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageController : MonoBehaviour
{
    public GameObject passageConnection;

    bool waitForPlayerMove;
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

    void GoToOtherRoom(GameObject player)
    {
        if (!passageConnection) return;

        GameObject levelControllerObj = GameObject.FindGameObjectWithTag("GameController");
        LevelController levelController = levelControllerObj.GetComponent<LevelController>();

        if (levelController.GetIsInRoomTransition()) return;

        waitForPlayerMove = true;

        CharacterController playerCharacterController = player.GetComponent<CharacterController>();

        playerCharacterController.enabled = false;
        player.transform.position = passageConnection.transform.position;
        playerCharacterController.enabled = true;

        Transform nextRoom = passageConnection.transform.parent;
        int roomIndex = nextRoom.GetComponent<RoomController>().roomIndex;

        levelController.UpdateCurrentRoom(roomIndex);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") waitForPlayerMove = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" || waitForPlayerMove || !roomController.IsAllEnemiesDied())
        {
            return;
        }

        GoToOtherRoom(other.gameObject);
    }
}
