using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShopperController : MonoBehaviour
{
    [SerializeField] Transform cartSpawnPoints;
    private List<BotShoppingListItem> botShoppingList;
    public bool finishedShopping = false;
    public bool startInside = false;

    private int currentItemIndex = 0;
    private bool searchingForItem = false;
    private bool grabbingItem = false;
    private bool leaving = false;

    private void Start() {
        botShoppingList = new List<BotShoppingListItem>();
        int numberOfItems = Random.Range(2, 12);

        GameObject[] pickUpAreaArray = GameObject.FindGameObjectsWithTag(Tag.PICK_UP_AREA);
        for (int i = 0; i < numberOfItems; i++) {
            int itemIndex = Random.Range(0, pickUpAreaArray.Length);
            GameObject pickUpArea = pickUpAreaArray[itemIndex];

            ItemGrabArea itemGrabArea = pickUpArea.GetComponent<ItemGrabArea>();
            Item item = itemGrabArea.item;
            BotShoppingListItem botShoppingListItem = new BotShoppingListItem(item, itemGrabArea.transform);
            botShoppingList.Add(botShoppingListItem);
        }
    }

    private void Update() {
        
    }
}
