using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireGunHands : MonoBehaviour
{
    public GameObject fireGunLeftHand;
    public GameObject fireGunRightHand;

    void Update()
    {
        PointToMouse();
    }

    Vector3 CalculeDirection(Vector3 target, Vector3 position)
    {
        return target - position;
    }

    float CalculeRotation(float directionX, float directionZ)
    {
        return Mathf.Atan2(directionX, directionZ) * Mathf.Rad2Deg - 70;
    }

    Quaternion CalculeQuaternionEuler(float rotation)
    {
        return Quaternion.Euler(
            0,
            rotation,
            Time.deltaTime * 0.5f
        );
    }

    void PointToMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(cameraRay, out distance))
        {
            Vector3 target = cameraRay.GetPoint(distance);
            Vector3 leftHandDirection = CalculeDirection(target, fireGunLeftHand.transform.position);
            Vector3 rightHandDirection = CalculeDirection(target, fireGunRightHand.transform.position);

            float leftHandRotation = CalculeRotation(leftHandDirection.x, leftHandDirection.z);
            float rightHandRotation = CalculeRotation(rightHandDirection.x, rightHandDirection.z);

            fireGunLeftHand.transform.rotation = CalculeQuaternionEuler(leftHandRotation);
            fireGunRightHand.transform.rotation = CalculeQuaternionEuler(rightHandRotation);
        }
    }
}
