using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class LifeBarController
{
    public Slider lifeBarSlider;
    public Image lifeBarSliderFill;

    Color greenColor = new Color(0.2f, 1f, 0f, 0.4f);
    Color yellowColor = new Color(1f, 1f, 0f, 0.4f);
    Color redColor = new Color(1f, 0f, 0f, 0.4f);

    public void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        PlayerController playerController = playerObject.GetComponent<PlayerController>();

        UpdateLifeBar(playerController.life);
    }

    Color GetLifeBarColor(int updatedLife)
    {
        return updatedLife > 66 ? greenColor :
            updatedLife > 33 ? yellowColor :
            redColor;
    }

    public void UpdateLifeBar(int updatedLife)
    {
        lifeBarSlider.value = updatedLife;

        lifeBarSliderFill.color = GetLifeBarColor(updatedLife);
    }
}

[Serializable]
public class MinimapController
{
    const string TOGGLE_MINIMAP_FULL_SCREEN_BUTTON = "MinimapFullScreen";
    const int DEFAULT_LOWER_ROOM_Y_POSITION = 0;
    const int DEFAULT_MINIMAP_CAM_ORTHOGRAPHIC_SIZE = 12;
    Vector3 DEFAULT_MINIMAP_CAM_POSITION = new Vector3(0, 20, 0);

    public RectTransform minimapRectTransform;
    public Transform minimapCameraTransform;
    public bool isFullScreen;

    int smallSize = 400, fullScreenSize = 1000;
    Vector2 smallPosition = new Vector2(-220, -220);
    Vector2 fullScreenPosition = new Vector2(-960, -540);
    Vector3 minimapCameraPosition;
    LevelBuilder levelBuilder;
    Camera minimapCamera;

    public void Start()
    {
        minimapCamera = minimapCameraTransform.GetComponent<Camera>();
        minimapCamera.orthographicSize = DEFAULT_MINIMAP_CAM_ORTHOGRAPHIC_SIZE;
        minimapCameraPosition = DEFAULT_MINIMAP_CAM_POSITION;
    }

    public void Update()
    {
        if (Input.GetButtonDown(TOGGLE_MINIMAP_FULL_SCREEN_BUTTON))
        {
            ToggleFullScreen();
        }

        if (isFullScreen) minimapCameraTransform.position = minimapCameraPosition;
        else minimapCameraTransform.localPosition = minimapCameraPosition;
    }

    public void ToggleFullScreen()
    {
        isFullScreen = !isFullScreen;

        UpdateCameraPosition();
        UpdateMinimapCanvasPosition();
    }

    void UpdateCameraPosition()
    {
        if (!isFullScreen)
        {
            minimapCameraPosition = DEFAULT_MINIMAP_CAM_POSITION;
            minimapCamera.orthographicSize = DEFAULT_MINIMAP_CAM_ORTHOGRAPHIC_SIZE;
            return;
        }

        Vector2[] roomPositions = levelBuilder.GetRoomPositions();

        if (roomPositions.Length == 0) return;

        float minX = float.PositiveInfinity, maxX = float.NegativeInfinity, maxY = float.NegativeInfinity;

        foreach (Vector2 roomPosition in roomPositions)
        {
            minX = Mathf.Min(minX, roomPosition.x);
            maxX = Mathf.Max(maxX, roomPosition.x);
            maxY = Mathf.Max(maxY, roomPosition.y);
        }

        float middleX = (maxX + minX) / 2;
        float middleY = maxY / 2;

        float cameraHeight = Mathf.Max((maxX - minX) / 2, (maxY / 2));

        Vector3 levelCenterPosition = new Vector3(middleX, cameraHeight, middleY);

        minimapCameraPosition = levelCenterPosition;
        minimapCamera.orthographicSize = cameraHeight + 12;
    }


    void UpdateMinimapCanvasPosition()
    {
        Vector2 newPosition = isFullScreen ? fullScreenPosition : smallPosition;
        int newSize = isFullScreen ? fullScreenSize : smallSize;

        minimapRectTransform.anchoredPosition = newPosition;
        minimapRectTransform.sizeDelta = new Vector2(newSize, newSize);
    }

    public void SetLevelBuilder(LevelBuilder levelBuilderToSet)
    {
        levelBuilder = levelBuilderToSet;
    }
}

[Serializable]
public class AmmoInfo
{
    public TextMeshProUGUI hudAmmoText;

    public void UpdateAmmoInfo(int currentAmmo, int maxAmmo)
    {
        hudAmmoText.fontSize = 75;
        hudAmmoText.text = $"{currentAmmo}/{maxAmmo}";
    }

    public void ClearAmmoInfo()
    {
        hudAmmoText.text = "";
    }

    public void ReloadingText()
    {
        hudAmmoText.fontSize = 60;
        hudAmmoText.text = "Reloading...";
    }
}

public class HUDController : MonoBehaviour
{
    const string GAME_CONTROLLER_TAG = "GameController";

    public LifeBarController lifeBarController;
    public AmmoInfo ammoInfo;
    public MinimapController minimapController;

    GameObject gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag(GAME_CONTROLLER_TAG);

        minimapController.SetLevelBuilder(gameController.GetComponent<LevelBuilder>());
        minimapController.Start();

        lifeBarController.Start();
    }

    void Update()
    {
        minimapController.Update();
    }

    public void UpdateLifeBar(int updatedLife)
    {
        lifeBarController.UpdateLifeBar(updatedLife);
    }

    public void UpdateAmmoInfo(int currentAmmo, int maxAmmo)
    {
        ammoInfo.UpdateAmmoInfo(currentAmmo, maxAmmo);
    }
}
