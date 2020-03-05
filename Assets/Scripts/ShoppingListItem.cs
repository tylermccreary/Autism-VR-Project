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
    private bool collected;
    
    public delegate void ItemClickEvent(Item item);
    public static event ItemClickEvent OnItemClickEvent;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tag.FINGER) {
            OnItemClickEvent(this.item);
        }
    }
}
