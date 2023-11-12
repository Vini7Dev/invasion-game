using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjectsContainer : MonoBehaviour
{
    bool isChildrenActive = true;
    RoomController roomController;

    void Start()
    {
        GameObject roomObject = transform.parent.gameObject;
        roomController = roomObject.GetComponent<RoomController>();
    }

    void Update()
    {
        if (roomController.GetIsRoomActive() != isChildrenActive)
        {
            ActiveOrDeactiveChildren(roomController.GetIsRoomActive());
        }
    }

    void ActiveOrDeactiveChildren(bool isRoomActive)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isRoomActive);
        }

        isChildrenActive = isRoomActive;
    }
}
