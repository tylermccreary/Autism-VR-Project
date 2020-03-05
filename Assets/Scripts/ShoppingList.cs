using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingList : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    List<ShoppingListItem> list = new List<ShoppingListItem>();

    private void Start() {
        CreateRandomizedShoppingList();
    }

    private void CreateRandomizedShoppingList() {
        //TODO

    }
}
