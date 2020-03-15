using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShoppingListItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Item item;
    [SerializeField] private int quantity;
    private string name;
    public Transform itemAreaTransform;
    private bool collected;
    
    public delegate void ItemClickEvent(Item item, Transform areaTransform);
    public static event ItemClickEvent OnItemClickEvent;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tag.FINGER && this.item != null) {
            OnItemClickEvent(this.item, itemAreaTransform);
        }
    }

    public void SetUpListItem(Item item, int quantity, Transform areaTransform) {
        this.item = item;
        this.quantity = quantity;
        this.itemAreaTransform = areaTransform;

        if (item.ItemSpecific != ItemSpecificEnum.NA) {
            this.name = item.ItemSpecific.ToString() + " " + item.Name.ToString();
        } else {
            if (item.Name != ItemNameEnum.Unknown) {
                this.name = item.Name.ToString();
            } else {
                this.name = item.ItemType.ToString();
            }
        }
        
        this.text.text = this.name;
        gameObject.SetActive(true);
    }

    public Item GetItem() {
        return item;
    }

    public void IncrementQuantity() {
        quantity += 1;
        this.text.text = this.text.text;
        this.text.text = this.name + " (" + quantity + ")";
    }
}
