using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryGun : FireGun
{
    SpriteRenderer gunSprite;

    void Awake()
    {
        base.Awake();
        gunSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        base.Update();
        HideSpriteWhenEmptyBullets();
    }

    void HideSpriteWhenEmptyBullets()
    {
        gunSprite.enabled = bullets > 0;
    }

    public void AddOneBullet()
    {
        bullets += 1;
    }
}
