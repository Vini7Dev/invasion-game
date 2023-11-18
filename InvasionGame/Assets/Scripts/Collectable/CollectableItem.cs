using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Health
}

public class CollectableItem : MonoBehaviour
{
    public ItemType itemType = ItemType.Health;
    public int buffValue;
}
