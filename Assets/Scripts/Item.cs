using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemNameEnum {
    Carrot
}

public enum ItemTypeEnum {
    Fruit,
    Vegetable
}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemNameEnum name;
    [SerializeField] private ItemTypeEnum itemType;
    public ItemNameEnum Name { get { return name; } set { name = value; } }
    public ItemTypeEnum ItemType { get { return itemType; } set { itemType = value; } }
}
