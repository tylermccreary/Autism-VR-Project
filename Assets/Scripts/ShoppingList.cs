using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingList : MonoBehaviour {
    [SerializeField] private List<Button> buttons;
    List<ShoppingListItem> list = new List<ShoppingListItem>();

    private void Start() {
        foreach (Button button in buttons) {
            list.Add(button.GetComponent<ShoppingListItem>());
        }
        CreateRandomizedShoppingList();
    }

    private void CreateRandomizedShoppingList() {
        //TODO
        GameObject[] array = GameObject.FindGameObjectsWithTag(Tag.PICK_UP_AREA);
        Debug.Log(array.Length);
        ItemGrabArea item0 = array[0].GetComponent<ItemGrabArea>();
        list[0].SetUpListItem(item0.item.Name.ToString(), item0.item, 1, item0.transform);
        ItemGrabArea item1 = array[1].GetComponent<ItemGrabArea>();
        list[1].SetUpListItem(item1.item.Name.ToString(), item1.item, 1, item1.transform);
        ItemGrabArea item2 = array[2].GetComponent<ItemGrabArea>();
        list[2].SetUpListItem(item2.item.Name.ToString(), item2.item, 1, item2.transform);
        ItemGrabArea item3 = array[3].GetComponent<ItemGrabArea>();
        list[3].SetUpListItem(item3.item.Name.ToString(), item3.item, 1, item3.transform);
    }
}
