using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    public GameObject pauseMenuUI;

    bool isPaused;
    List<GameObject> entitiesInGame = new List<GameObject>();
    List<GameObject> weaponsInGame = new List<GameObject>();
    List<GameObject> projectilesInGame = new List<GameObject>();
    List<GameObject> trapsInGame = new List<GameObject>();
    GameObject hudControllerObject;
    AudioSource gameMusiceSource;

    void Start()
    {
        gameMusiceSource = GetComponent<AudioSource>();
        hudControllerObject = GameObject.FindGameObjectWithTag(GlobalTags.HUD_CONTROLLER);
    }

    void Update()
    {
        if (Input.GetButtonDown(GlobalButtons.PAUSE_GAME)) TogglePauseGame();
    }

    void TogglePauseGame()
    {
        isPaused = !isPaused;

        pauseMenuUI.SetActive(isPaused);
        gameMusiceSource.volume = isPaused ? 0.2f : 0.5f;

        TogglePausedObjectScripts();
    }

    void TogglePausedObjectScripts()
    {
        PauseScript<HUDController>(hudControllerObject);

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

        foreach (GameObject projectile in projectilesInGame)
        {
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
        GameObject player = GameObject.FindGameObjectWithTag(GlobalTags.PLAYER);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(GlobalTags.ENEMY);
        GameObject[] weapons = GameObject.FindGameObjectsWithTag(GlobalTags.WEAPON);
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag(GlobalTags.PROJECTILE);
        GameObject[] traps = GameObject.FindGameObjectsWithTag(GlobalTags.TRAP);

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

        if (scriptToPause != null)
        {
            scriptToPause.enabled = !isPaused;
        }
    }
}
