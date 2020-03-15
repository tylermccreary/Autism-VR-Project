using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotShopperController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] cartSpawnPositions;
    private List<BotShoppingListItem> botShoppingList;
    public bool finishedShopping = false;

    private int currentItemIndex = 0;
    private bool searchingForItem = true;
    private bool grabbingItem = false;
    private bool leaving = false;
    private bool itemGrabbed = false;

    private float timeGrabbingItem = 0f;
    private float timeToGrabItem = 2f;

    private void Start() {
        botShoppingList = new List<BotShoppingListItem>();
        int numberOfItems = Random.Range(2, 8);

        GameObject[] pickUpAreaArray = GameObject.FindGameObjectsWithTag(Tag.PICK_UP_AREA);
        for (int i = 0; i < numberOfItems; i++) {
            int itemIndex = Random.Range(0, pickUpAreaArray.Length);
            GameObject pickUpArea = pickUpAreaArray[itemIndex];

            ItemGrabArea itemGrabArea = pickUpArea.GetComponent<ItemGrabArea>();
            Item item = itemGrabArea.item;
            BotShoppingListItem botShoppingListItem = new BotShoppingListItem(item, itemGrabArea.transform);
            botShoppingList.Add(botShoppingListItem);
        }
        navMeshAgent.destination = botShoppingList[currentItemIndex].itemGrabArea.position;
    }

    private void Update() {
        if (searchingForItem) {
            CheckIfAtItem();
        } else if (grabbingItem) {
            GrabItem();
        } else if (leaving) {
            Leave();
        }
    }

    private void CheckIfAtItem() {
        if ((transform.position - navMeshAgent.destination).magnitude < 2f) {
            navMeshAgent.isStopped = false;
            grabbingItem = true;
            searchingForItem = false;
        }
    }

    private void GrabItem() {
        timeGrabbingItem += Time.deltaTime;
        if (!itemGrabbed) {
            PutItemInCart();
        }

        if (timeGrabbingItem > timeToGrabItem) {
            timeGrabbingItem = 0;
            grabbingItem = false;
            NextItem();
        }
    }

    private void Leave() {
        GameObject botSpawner = GameObject.FindGameObjectWithTag(Tag.BOT_SPAWNER);
        navMeshAgent.destination = botSpawner.transform.position;
        Debug.Log("LEAVING");
    }

    private void PutItemInCart() {
        navMeshAgent.isStopped = true;
        Transform spawnPosition = cartSpawnPositions[Random.Range(0, cartSpawnPositions.Length)];
        GameObject spawnedItem = Instantiate(botShoppingList[currentItemIndex].itemGrabArea.gameObject.GetComponent<ItemGrabArea>().prefabToSpawn, spawnPosition);
        //spawnedItem.transform.parent = null;
        itemGrabbed = true;
    }

    private void NextItem() {
        if (currentItemIndex >= botShoppingList.Count - 1) {
            leaving = true;
            finishedShopping = true;
            navMeshAgent.isStopped = false;
        } else {
            currentItemIndex++;
            searchingForItem = true;
            navMeshAgent.destination = botShoppingList[currentItemIndex].itemGrabArea.position;
            navMeshAgent.isStopped = false;
            itemGrabbed = false;
        }
    }
}
