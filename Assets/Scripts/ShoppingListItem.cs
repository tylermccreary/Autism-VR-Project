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
    public Transform itemAreaTransform;
    private bool collected;
    
    public delegate void ItemClickEvent(Item item, Transform areaTransform);
    public static event ItemClickEvent OnItemClickEvent;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tag.FINGER && this.item != null) {
            OnItemClickEvent(this.item, itemAreaTransform);
        }
    }

    public void SetUpListItem(string text, Item item, int quantity, Transform areaTransform) {
        Debug.Log(text);
        this.text.text = text;
        this.item = item;
        this.quantity = quantity;
        this.itemAreaTransform = areaTransform;
    }
}
