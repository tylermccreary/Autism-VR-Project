using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemSpecificEnum {
    NA,
    Apple,
    Orange,
    Chocolate,
    Laundry,
    Dish,
    Cod,
    Tuna,
    Herring,
    Saury,
    Perch,
    Raspberry
}

public enum ItemNameEnum {
    Apple,
    Artichoke,
    Banana,
    Beer,
    Beet,
    Juice,
    Unknown,
    Milk,
    Water,
    Cereal,
    Candy,
    Tea,
    Detergent,
    Bread,
    Broccoli,
    Cabbage,
    Corn,
    Fish,
    Peas,
    Carrot,
    Cauliflower,
    Cheese,
    Cucumber,
    Eggplant,
    Garlic,
    IceCream,
    Lemon,
    Meat,
    Melon,
    Mushroom,
    Onion,
    Orange,
    Chips,
    Coffee,
    Pasta,
    Papaya,
    Pear,
    Pepper,
    Pineapple,
    Potato,
    Pumpkin,
    Squash,
    Tomato,
    Watermelon
}

public enum ItemTypeEnum {
    Beverage,
    Fruit,
    Vegetable,
    Grain,
    Sweet,
    Cleaning,
    Protein,
    Dairy,
    Snack
}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemNameEnum name;
    [SerializeField] private ItemTypeEnum itemType;
    [SerializeField] private ItemSpecificEnum specific;
    public ItemNameEnum Name { get { return name; } set { name = value; } }
    public ItemTypeEnum ItemType { get { return itemType; } set { itemType = value; } }
    public ItemSpecificEnum ItemSpecific { get { return specific; } set { specific = value; } }

    public static bool Equals(Item item1, Item itemSpecific) {
        if (item1.ItemType == itemSpecific.ItemType) {
            if (itemSpecific.Name == ItemNameEnum.Unknown) {
                return true;
            } else {
                if (item1.Name == itemSpecific.Name) {
                    if (itemSpecific.ItemSpecific == ItemSpecificEnum.NA) {
                        return true;
                    } else {
                        if (item1.ItemSpecific == itemSpecific.ItemSpecific) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                } else {
                    return false;
                }
            }
        } else {
            return false;
        }
    }
}
