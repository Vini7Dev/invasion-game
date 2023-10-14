using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected const string ENEMY_TAG = "Enemy";
    protected const string PLAYER_TAG = "Player";

    public bool isPlayerAttack;
    public int maxDamage = 10, minDamage = 5;

    int GetRandomDamage() {
        return Random.Range(minDamage, maxDamage + 1);
    }

    public void ApplyDamage(GameObject other) {
        EntityController entityController = other.GetComponent<EntityController>();
        entityController.HaveHitADamage(GetRandomDamage());
    }
}
