using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWiteGunHands : MonoBehaviour
{
    public GameObject witeGunHands;

    void Update()
    {
        PointToMouse();
    }

    void PointToMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(cameraRay, out distance))
        {
            Vector3 target = cameraRay.GetPoint(distance);
            Vector3 direction = target - witeGunHands.transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            witeGunHands.transform.rotation = Quaternion.Euler(
                0,
                rotation,
                Time.deltaTime * 0.5f
            );
        }
    }
}
