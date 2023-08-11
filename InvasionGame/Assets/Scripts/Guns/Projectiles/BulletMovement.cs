using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : ProjectileMovement
{
    Vector3 forwardDirection = Vector3.right;

    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(forwardDirection * projectileSpeed * Time.deltaTime);
    }
}
