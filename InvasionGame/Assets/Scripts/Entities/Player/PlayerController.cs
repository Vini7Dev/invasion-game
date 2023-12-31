using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    static bool alive = true;

    HUDController hudController;

    void Start()
    {
        base.Start();

        GameObject hudControllerObject = GameObject.FindGameObjectWithTag(GlobalTags.HUD_CONTROLLER);
        hudController = hudControllerObject.GetComponent<HUDController>();
    }

    void Update()
    {
        base.Update();
    }

    public static void SetAlive(bool setAlive)
    {
        alive = setAlive;
    }

    public override void AddLife(int lifeToAdd)
    {
        if (life + lifeToAdd > entitySkills.maxLife)
        {
            life = entitySkills.maxLife;
        }
        else
        {
            life += lifeToAdd;
        }

        hudController.UpdateLifeBar(life);
    }

    protected override void WhenTakingDamage(GameObject _)
    {
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
