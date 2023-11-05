using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    static bool alive = true;

    HUDController hudController;

    void Start()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        hudController = gameController.GetComponent<HUDController>();
    }

    void Update()
    {
        base.Update();
    }

    public static void SetAlive(bool setAlive)
    {
        alive = setAlive;
    }

    protected override void WhenTakingDamage(GameObject _) {
        hudController.UpdateLifeBar(life);
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
