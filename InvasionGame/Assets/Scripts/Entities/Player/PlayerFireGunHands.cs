using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireGunHands : EntityFireGunHands
{
    void Update()
    {
        PointToMouse();

        base.Update();
    }

    Vector3 CalculeDirection(Vector3 target, Vector3 position)
    {
        return target - position;
    }

    float CalculeRotation(float directionX, float directionZ)
    {
        return Mathf.Atan2(directionX, directionZ) * Mathf.Rad2Deg;
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

            PointFireGunHandToMouse(fireGunLeftHand, target);
            PointFireGunHandToMouse(fireGunRightHand, target);
            UpdateFireGunHandsScale(fireGunHands, target);
        }
    }

    void PointFireGunHandToMouse(GameObject handToPoint, Vector3 target)
    {
        Vector3 handDirection = CalculeDirection(target, handToPoint.transform.position);

        float handRotation = CalculeRotation(handDirection.x, handDirection.z);

        handToPoint.transform.rotation = CalculeQuaternionEuler(handRotation);
    }

    void UpdateFireGunHandsScale(GameObject fireGunHands, Vector3 target)
    {
        Vector3 fireGunHandsDirection = CalculeDirection(target, fireGunHands.transform.position);

        float fireGunHandsRotation = CalculeRotation(fireGunHandsDirection.x, fireGunHandsDirection.z);

        int fireGunHandsRotationMultiplier = fireGunHandsRotation < 0 ? -1 : 1;

        Vector3 fuireGunHandsScale = new Vector3(
            Math.Abs(transform.localScale.x) * fireGunHandsRotationMultiplier,
            transform.localScale.y,
            transform.localScale.z
        );

        fireGunHands.transform.localScale = fuireGunHandsScale;
    }
}
