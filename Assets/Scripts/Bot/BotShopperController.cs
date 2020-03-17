using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotShopperController : MonoBehaviour {

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] cartSpawnPositions;
    [SerializeField] private GameObject cartObject;

    private enum StateEnum { SearchingForItem, GrabbingItem, CheckingOut, Leaving }
    private StateEnum state = StateEnum.SearchingForItem;

    private List<BotShoppingListItem> botShoppingList;
    public bool finishedShopping = false;

    private int currentItemIndex = 0;
    private bool itemGrabbed = false;

    private float timeGrabbingItem = 0f;
    private float timeToGrabItem = 2f;
    private float timeCheckingOut = 0f;
    private float timeToCheckOut = 4f;
    private bool laneSelected = false;

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
        switch (state) {
            case StateEnum.SearchingForItem:
                CheckIfAtItem();
                break;
            case StateEnum.GrabbingItem:
                GrabItem();
                break;
            case StateEnum.CheckingOut:
                CheckOut();
                break;
            case StateEnum.Leaving:
                Leave();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == Tag.BOT_CART_AREA) {
            ShowCart();
        }
    }

    private void CheckIfAtItem() {
        if ((transform.position - navMeshAgent.destination).magnitude < 2f) {
            navMeshAgent.isStopped = false;
            state = StateEnum.GrabbingItem;
        }
    }

    private void GrabItem() {
        timeGrabbingItem += Time.deltaTime;
        if (!itemGrabbed) {
            PutItemInCart();
        }

        if (timeGrabbingItem > timeToGrabItem) {
            timeGrabbingItem = 0;
            NextItem();
        }
    }

    private void CheckOut() {
        if (!laneSelected) {
            GameObject[] checkoutLanes = GameObject.FindGameObjectsWithTag(Tag.CHECKOUT_LANE);
            navMeshAgent.destination = checkoutLanes[Random.Range(0, checkoutLanes.Length)].transform.position;
            navMeshAgent.isStopped = false;
            laneSelected = true;
        }

        if ((transform.position - navMeshAgent.destination).magnitude < 1f) {
            navMeshAgent.isStopped = true;
            timeCheckingOut += Time.deltaTime;
            if (timeCheckingOut > timeToCheckOut) {
                state = StateEnum.Leaving;
                timeCheckingOut = 0f;
            }
        }
    }

    private void Leave() {
        navMeshAgent.isStopped = false;
        GameObject botSpawner = GameObject.FindGameObjectWithTag(Tag.BOT_SPAWNER);
        navMeshAgent.destination = botSpawner.transform.position;
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
            finishedShopping = true;
            state = StateEnum.CheckingOut;
        } else {
            currentItemIndex++;
            state = StateEnum.SearchingForItem;
            navMeshAgent.destination = botShoppingList[currentItemIndex].itemGrabArea.position;
            navMeshAgent.isStopped = false;
            itemGrabbed = false;
        }
    }

    public void ShowCart() {
        cartObject.SetActive(true);
    }
}
