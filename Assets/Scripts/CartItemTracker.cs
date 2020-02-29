using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartItemTracker : MonoBehaviour
{
    private List<Item> items;
    public List<Item> ItemsList { get { return items; } }

    public delegate void CartContentsChangeEvent();
    public static event CartContentsChangeEvent OnCartContentsChange;

    private void Awake() {
        items = new List<Item>();
    }

    private void OnTriggerEnter(Collider other) {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null) {
            items.Add(item);
        }
    }

    private void OnTriggerExit(Collider other) {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null) {
            items.Remove(item);
        }
    }
}
