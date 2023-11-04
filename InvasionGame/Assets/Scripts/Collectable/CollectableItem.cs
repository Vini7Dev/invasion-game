using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Item,
    FireGun,
    WhiteGun
}

public class CollectableItem : MonoBehaviour
{
    public GameObject collectableObject;
    public ItemType itemType = ItemType.Item;
    public float zPositionRelative = 0;
}
