using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShoppingListItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI tmpGUI;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Item item;
    [SerializeField] private int quantity;
    private string name;
    public ItemGrabArea itemGrabArea;
    private bool collected;
    
    public delegate void ItemClickEvent(Item item, ItemGrabArea areaTransform);
    public static event ItemClickEvent OnItemClickEvent;

    private void OnEnable() {
        CartItemTracker.OnCartContentsAdd += CheckItemInCart;
        CartItemTracker.OnCartContentsDelete += CheckItemOutOfCart;
    }

    private void OnDisable() {
        CartItemTracker.OnCartContentsAdd -= CheckItemInCart;
        CartItemTracker.OnCartContentsDelete -= CheckItemOutOfCart;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tag.FINGER && this.item != null) {
            OnItemClickEvent(this.item, itemGrabArea);
        }
    }

    public void SetUpListItem(Item item, int quantity, ItemGrabArea itemGrabArea) {
        this.item = item;
        this.quantity = quantity;
        this.itemGrabArea = itemGrabArea;

        if (item.ItemSpecific != ItemSpecificEnum.NA) {
            this.name = item.ItemSpecific.ToString() + " " + item.Name.ToString();
        } else {
            if (item.Name != ItemNameEnum.Unknown) {
                this.name = item.Name.ToString();
            } else {
                this.name = item.ItemType.ToString();
            }
        }
        
        this.tmpGUI.text = this.name;
        gameObject.SetActive(true);
    }

    public Item GetItem() {
        return item;
    }

    private void CheckItemInCart(Item item) {
        if (Item.Equals(this.item, item)) {
            DecrementQuantity();
        }
    }

    private void CheckItemOutOfCart(Item item) {
        if (Item.Equals(this.item, item)) {
            IncrementQuantity();
            Debug.Log("Increment");
        }
    }

    public void IncrementQuantity() {
        quantity += 1;
        if (quantity > 1) {
            this.tmpGUI.text = this.name + " (" + quantity + ")";
        } else {
            this.tmpGUI.text = this.name;
        }

        if (quantity <= 0) {
            this.text.fontStyle = FontStyles.Strikethrough;
        } else {
            this.text.fontStyle = FontStyles.Bold;
        }
    }

    public void DecrementQuantity() {
        quantity -= 1;
        if (quantity > 1) {
            this.tmpGUI.text = this.name + " (" + quantity + ")";
        } else {
            this.tmpGUI.text = this.name;
        }

        if (quantity <= 0) {
            this.text.fontStyle = FontStyles.Strikethrough;
        } else {
            this.text.fontStyle = FontStyles.Bold;
        }
    }
}
