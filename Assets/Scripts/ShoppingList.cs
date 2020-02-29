using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    List<ShoppingListItem> list = new List<ShoppingListItem>();

    private void Start() {
        CreateRandomizedShoppingList();
    }

    private void CreateRandomizedShoppingList() {
        //TODO
    }
}
