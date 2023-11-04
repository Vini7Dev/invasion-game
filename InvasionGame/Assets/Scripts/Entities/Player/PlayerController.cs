using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : EntityController
{
    static bool alive = true;

    void Update()
    {
        base.Update();
    }

    public static void SetAlive(bool setAlive)
    {
        alive = setAlive;
    }

    protected override void WhenDying()
    {
        PlayerController.SetAlive(false);

        base.WhenDying();
    }

    public static bool IsAlive()
    {
        return alive;
    }
}
