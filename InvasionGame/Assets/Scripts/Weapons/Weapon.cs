using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected const string HUD_CONTROLLER_TAG = "HUDController";
    protected const string ENEMY_TAG = "Enemy";
    protected const string PLAYER_TAG = "Player";
    protected const string BREAKABLE_SCENERY_TAG = "BreakableScenery";

    public bool isPlayerAttack;
    public int maxDamage = 10, minDamage = 5;

    protected HUDController hudController;

    protected void Start()
    {
        if (isPlayerAttack)
        {
            GameObject hudControllerObject = GameObject.FindGameObjectWithTag(HUD_CONTROLLER_TAG);
            hudController = hudControllerObject.GetComponent<HUDController>();
        }
    }

    int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    public void ApplyDamage(GameObject other)
    {
        EntityController entityController = other.GetComponent<EntityController>();
        entityController.HaveHitADamage(GetRandomDamage(), gameObject);
    }
}
