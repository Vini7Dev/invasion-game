using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum Direction
{
    Up,
    Left,
    Right
}

[Serializable]
public class NextRoomPosition
{
    public Direction direction = Direction.Up;
    public Vector2 position, roomPassagePosition = new Vector2(0, 0);

    public bool IsDirection(Direction directionToCheck)
    {
        return direction == directionToCheck;
    }
}

public class RoomController : MonoBehaviour
{
    public MeshRenderer roomCover;
    public NextRoomPosition[] nextRoomPositions;
    public GameObject[] roomVariations, bonusRoomVariations;
    public GameObject sceneryObjects, treasureChestMinimap;
    public Animator roomMinimapAnimator;
    public int roomIndex;
    public bool isBonusRoom;

    bool isRoomActive;
    int totalOfEnemiesOnRoom;
    float roomCoverAnimationSpeed, roomCoverTransitionSpeed = 0.5f;
    Material roomCoverMaterial;
    GameObject roomVariationObject;

    void Start()
    {
        roomCoverMaterial = roomCover.materials[0];
        ProcessCurrentRoom();
        ToggleActiveOfScenaryAndVariation();

        if (isBonusRoom) treasureChestMinimap.SetActive(true);
    }

    void Update()
    {
        OpenOrCloseRoomCover();
    }

    GameObject GetRoomVariant()
    {
        GameObject[] variantsArray = !isBonusRoom ? roomVariations : bonusRoomVariations;

        int roomVariationIndex = 0;

        if (roomIndex != 0)
        {
            roomVariationIndex = UnityEngine.Random.Range(0, variantsArray.Length);
        }

        return variantsArray[roomVariationIndex];
    }

    void ProcessCurrentRoom()
    {
        GameObject currentRoomVariation = GetRoomVariant();

        GameObject roomVariationInstance = Instantiate(
            currentRoomVariation,
            transform.position,
            currentRoomVariation.transform.rotation
        );

        roomVariationObject = roomVariationInstance;

        RoomVariation roomVariation = roomVariationObject.GetComponent<RoomVariation>();

        roomVariation.roomController = GetComponent<RoomController>();
    }

    void OpenOrCloseRoomCover()
    {
        if ((isRoomActive && roomCoverMaterial.color.a > 0)
            || (!isRoomActive && roomCoverMaterial.color.a < 1)
        )
        {
            float currentAlpha = roomCoverMaterial.color.a;

            roomCoverMaterial.color = new Color(0, 0, 0, currentAlpha + roomCoverAnimationSpeed);

            currentAlpha = roomCoverMaterial.color.a;

            if (isRoomActive && currentAlpha < 0)
                roomCoverMaterial.color = new Color(0, 0, 0, 0);
            else if (!isRoomActive && currentAlpha > 1)
                roomCoverMaterial.color = new Color(0, 0, 0, 1);
        }
    }

    public void SetIsRoomActive(bool newIsActive)
    {
        isRoomActive = newIsActive;
        roomCoverAnimationSpeed = (isRoomActive ? -1 : 5) * Time.deltaTime * roomCoverTransitionSpeed;

        if (isRoomActive)
        {
            ToggleActiveOfScenaryAndVariation();
            if (isBonusRoom) OnAllEnemiesAreKilled();
        }
        else
        {
            StartCoroutine(HideSceneryObjects());
        }
    }

    void ToggleActiveOfScenaryAndVariation()
    {
        sceneryObjects.SetActive(isRoomActive);

        if (roomVariationObject != null)
        {
            roomVariationObject.SetActive(isRoomActive);
        }
    }

    void OnAllEnemiesAreKilled()
    {
        roomMinimapAnimator.SetTrigger("RoomCompleted");
    }

    public void IncrementTotalOfEnemiesOnRoom()
    {
        totalOfEnemiesOnRoom += 1;
    }

    public void OnEnemyDies()
    {
        totalOfEnemiesOnRoom -= 1;

        if (IsAllEnemiesDied()) OnAllEnemiesAreKilled();
    }

    public bool IsAllEnemiesDied()
    {
        return totalOfEnemiesOnRoom <= 0;
    }

    public bool GetIsRoomActive()
    {
        return isRoomActive;
    }

    IEnumerator HideSceneryObjects()
    {
        yield return new WaitForSeconds(roomCoverTransitionSpeed + 0.1f);

        ToggleActiveOfScenaryAndVariation();
    }
}
