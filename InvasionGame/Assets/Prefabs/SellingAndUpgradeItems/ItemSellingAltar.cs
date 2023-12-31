using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSellingAltar : MonoBehaviour
{
    const int SPRITE_Y_POSITION = 0;
    const int SPRITE_Z_SIZE = 1;

    public GameObject spriteOfItemForSale;

    ItemForSale itemForSale;

    void UpdateItemForSaleSprite()
    {
        SpriteRenderer spriteRenderer = spriteOfItemForSale.GetComponent<SpriteRenderer>();

        Transform spriteTransform = spriteOfItemForSale.transform;

        spriteRenderer.sprite = itemForSale.itemSprite;

        Vector3 spritePosition = new Vector3(
            itemForSale.itemSpritePosition.x,
            SPRITE_Y_POSITION,
            itemForSale.itemSpritePosition.y
        );

        spriteTransform.position = spriteTransform.position + spritePosition;

        spriteTransform.localScale = new Vector3(
            itemForSale.itemSpriteSize.x,
            itemForSale.itemSpriteSize.y,
            SPRITE_Z_SIZE
        );
    }

    public void SetItemForSale(ItemForSale itemForSaleToSet)
    {
        itemForSale = itemForSaleToSet;

        UpdateItemForSaleSprite();
    }

    public ItemForSale GetItemForSale()
    {
        return itemForSale;
    }
}
