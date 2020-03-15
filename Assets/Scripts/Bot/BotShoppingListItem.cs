using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoppingListItem
{
    public Item item;
    public Transform itemGrabArea;

    public BotShoppingListItem (Item item, Transform itemGrabArea) {
        this.item = item;
        this.itemGrabArea = itemGrabArea;
    }
}
