using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalTags
{
    public static string PLAYER = "Player";
    public static string ENEMY = "Enemy";

    public static string WEAPON = "Weapon";
    public static string PROJECTILE = "Projectile";

    public static string TRAP = "Trap";
    public static string OUT_OF_WALL = "OutOfWall";
    public static string BREAKABLE_SCENERY = "BreakableScenery";
    public static string INTERACTIVE_OBJECT = "InteractiveObject";

    public static string COLLECTABLE_WEAPON = "CollectableWeapon";
    public static string COLLECTABLE_ITEM = "CollectableItem";

    public static string HUD_CONTROLLER = "HUDController";
    public static string LEVEL_CONTROLLER = "GameController";
    public static string GAME_CONTROLLER = "GameController";
}

[System.Serializable]
public class GlobalButtons
{
    public static string HORIZONTAL = "Horizontal";
    public static string VERTICAL = "Vertical";
    public static string SHOT = "Fire1";
    public static string RELOAD = "Reload";
    public static string PAUSE_GAME = "PauseGame";
    public static string MINIMAP_FULL_SCREEN = "MinimapFullScreen";
}

public class GlobalVariables : MonoBehaviour
{}
