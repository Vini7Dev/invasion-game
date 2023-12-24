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
    const string TRAP_TAG = "Trap";

    public GameObject pauseMenuUI, hudController;

    bool isPaused;
    List<GameObject> entitiesInGame = new List<GameObject>();
    List<GameObject> weaponsInGame = new List<GameObject>();
    List<GameObject> projectilesInGame = new List<GameObject>();
    List<GameObject> trapsInGame = new List<GameObject>();

    void Update()
    {
        if (Input.GetButtonDown(PAUSE_GAME_BUTTON)) TogglePauseGame();
    }

    void TogglePauseGame()
    {
        isPaused = !isPaused;

        pauseMenuUI.SetActive(isPaused);
        PauseScript<HUDController>(hudController);

        if (isPaused) UpdateObjectsInGameList();

        foreach (GameObject entity in entitiesInGame)
        {
            PauseScript<PlayerMovement>(entity);
            PauseScript<EnemyMovement>(entity);
            PauseScript<EntityController>(entity);
            PauseScript<EntityWhiteGunHands>(entity);
            PauseScript<EntityFireGunHands>(entity);
            PauseScript<DealTouchDamage>(entity);
        }

        foreach (GameObject weapon in weaponsInGame) PauseScript<Weapon>(weapon);

        foreach (GameObject projectile in projectilesInGame) {
            PauseScript<ProjectileContainer>(projectile);
            PauseScript<ProjectileMovement>(projectile);
        }

        foreach (GameObject trap in trapsInGame)
        {
            PauseScript<DealTouchDamage>(trap);
            PauseScript<Animator>(trap);
        }
    }

    void UpdateObjectsInGameList()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        GameObject[] weapons = GameObject.FindGameObjectsWithTag(WEAPON_TAG);
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag(PROJECTILE_TAG);
        GameObject[] traps = GameObject.FindGameObjectsWithTag(TRAP_TAG);

        entitiesInGame = new List<GameObject>(enemies);
        entitiesInGame.Add(player);
        weaponsInGame = new List<GameObject>(weapons);
        projectilesInGame = new List<GameObject>(projectiles);
        trapsInGame = new List<GameObject>(traps);
    }

    void PauseScript<Script>(GameObject objectToPause) where Script : Behaviour
    {
        if (!objectToPause) return;

        Script scriptToPause = objectToPause.GetComponent<Script>();

        if (scriptToPause != null) {
            scriptToPause.enabled = !isPaused;
        }
    }
}
