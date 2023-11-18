using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariation : MonoBehaviour
{
    public RoomController roomController;

    bool isChildrenActive = true;

    void Update()
    {
        if (!roomController) return;

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
