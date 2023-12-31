using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemForSale
{
    public Sprite itemSprite;
    public Vector2 itemSpritePosition, itemSpriteSize;
}

public class SellingItemsController : MonoBehaviour
{
    public ItemSellingAltar[] itemSellingAltars;
    public ItemForSale[] itemsForSale;

    void Start()
    {
        foreach (ItemSellingAltar itemSellingAltar in itemSellingAltars)
        {
            ItemForSale randomItemToSale = GetRandomItemForSale();

            if (randomItemToSale == null) continue;

            itemsForSale = RemoveItemAtRandomIndex(itemsForSale, randomItemToSale);

            itemSellingAltar.SetItemForSale(randomItemToSale);
        }
    }

    ItemForSale GetRandomItemForSale()
    {
        return itemsForSale[Random.Range(0, itemsForSale.Length)];
    }

    ItemForSale[] RemoveItemAtRandomIndex(ItemForSale[] array, ItemForSale item)
    {
        List<ItemForSale> tempList = new List<ItemForSale>(array);
        tempList.Remove(item);
        return tempList.ToArray();
    }
}
