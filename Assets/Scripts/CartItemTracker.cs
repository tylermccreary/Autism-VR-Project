using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartItemTracker : MonoBehaviour
{
    private List<Item> items;
    public List<Item> ItemsList { get { return items; } }

    public delegate void CartContentsAddEvent(Item item);
    public static event CartContentsAddEvent OnCartContentsAdd;
    public delegate void CartContentsDeleteEvent(Item item);
    public static event CartContentsAddEvent OnCartContentsDelete;

    private void Awake() {
        items = new List<Item>();
    }

    private void OnTriggerEnter(Collider other) {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null) {
            items.Add(item);
            OnCartContentsAdd(item);
        }
    }

    private void OnTriggerExit(Collider other) {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null) {
            items.Remove(item);
            //OnCartContentsDelete(item);
        }
    }
}
