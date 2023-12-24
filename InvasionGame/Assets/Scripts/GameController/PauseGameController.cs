using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    const string PAUSE_GAME_BUTTON = "PauseGame";
    const string PLAYER_TAG = "Player";
    const string ENEMY_TAG = "Enemy";
    const string WEAPON_TAG = "Weapon";
    const string PROJECTILE_TAG = "Projectile";

    bool unpausedGame = true;
    List<GameObject> entitiesInGame = new List<GameObject>();
    List<GameObject> weaponsInGame = new List<GameObject>();
    List<GameObject> projectilesInGame = new List<GameObject>();

    void Update()
    {
        if (Input.GetButtonDown(PAUSE_GAME_BUTTON)) TogglePauseGame();
    }

    void TogglePauseGame()
    {
        unpausedGame = !unpausedGame;

        if (!unpausedGame) UpdateObjectsInGameList();

        foreach (GameObject entity in entitiesInGame)
        {
            if (!entity) continue;

            PauseEntityMovement(entity);
            PauseEntityWhiteGunHands(entity);
            PauseEntityFireGunHands(entity);
        }

        foreach (GameObject weapon in weaponsInGame) PauseWeapon(weapon);
        foreach (GameObject projectile in projectilesInGame) PauseProjectileMovement(projectile);
    }

    void UpdateObjectsInGameList()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        GameObject[] weapons = GameObject.FindGameObjectsWithTag(WEAPON_TAG);
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag(PROJECTILE_TAG);

        entitiesInGame = new List<GameObject>(enemies);
        entitiesInGame.Add(player);
        weaponsInGame = new List<GameObject>(weapons);
        projectilesInGame = new List<GameObject>(projectiles);
    }

    void PauseEntityMovement(GameObject entity)
    {
        EnemyMovement enemyMovement = entity.GetComponent<EnemyMovement>();

        if (enemyMovement)
        {
            PauseScript(enemyMovement);
            return;
        }

        PlayerMovement playerMovement = entity.GetComponent<PlayerMovement>();

        PauseScript(playerMovement);
    }

    void PauseEntityWhiteGunHands(GameObject entity)
    {
        EntityWhiteGunHands entityWhiteGunHands = entity.GetComponent<EntityWhiteGunHands>();

        PauseScript(entityWhiteGunHands);
    }

    void PauseEntityFireGunHands(GameObject entity)
    {
        EntityFireGunHands entityFireGunHands = entity.GetComponent<EntityFireGunHands>();

        PauseScript(entityFireGunHands);
    }

    void PauseWeapon(GameObject weaponObject)
    {
        Weapon weapon = weaponObject.GetComponent<Weapon>();

        PauseScript(weapon);
    }

    void PauseProjectileMovement(GameObject projectile)
    {
        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();

        PauseScript(projectileMovement);
    }

    void PauseScript(MonoBehaviour script)
    {
        if (script) script.enabled = unpausedGame;
    }
}
