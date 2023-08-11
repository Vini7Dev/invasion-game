using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryGun : FireGun
{
    SpriteRenderer gunSprite;

    void Awake()
    {
        base.Awake();
        base.isSecondaryGun = true;
        gunSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        base.Update();
        PointGunsToMouse();
        HideSpriteWhenEmptyBullets();
    }

    void HideSpriteWhenEmptyBullets()
    {
        gunSprite.enabled = bullets > 0;
    }

    void PointGunsToMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(cameraRay, out distance))
        {
            Vector3 target = cameraRay.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            base.projectileRotation = Quaternion.Euler(0, rotation, Time.deltaTime * 0.5f);
        }
    }

    public void AddOneBullet()
    {
        bullets += 1;
    }
}
