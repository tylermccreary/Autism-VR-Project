using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingList : MonoBehaviour {
    [SerializeField] private List<Button> buttons;
    List<ShoppingListItem> list = new List<ShoppingListItem>();
    private int listAmount;
    private int count = 0;

    private void Start() {
        listAmount = Random.Range(12, 21);
        for (int i = 0; i < listAmount ; i++) {
            list.Add(buttons[i].GetComponent<ShoppingListItem>());
        }
        CreateRandomizedShoppingList();
    }

    private void CreateRandomizedShoppingList() {
        GameObject[] objectsAvailable = GameObject.FindGameObjectsWithTag(Tag.PICK_UP_AREA);

        for (int i = 0; i < listAmount; i++) {
            int objectIndex = Random.Range(0, objectsAvailable.Length);
            GameObject objectSelected = objectsAvailable[objectIndex];

            ItemGrabArea itemSelected = objectSelected.GetComponent<ItemGrabArea>();

            bool incremented = false;
            for (int j = 0; j < list.Count; j++) {
                Item listItem = list[j].GetItem();
                if (listItem != null && Item.Equals(itemSelected.item, listItem)) {
                    list[j].IncrementQuantity();
                    incremented = true;
                    break;
                }
            }

            if (!incremented) {
                list[count].SetUpListItem(itemSelected.item, 1, itemSelected);
                count++;
            }

            incremented = false;
        }
    }
}
