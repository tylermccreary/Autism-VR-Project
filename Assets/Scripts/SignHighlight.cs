using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignHighlight : MonoBehaviour
{
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    public void OnEnable() {
        ShoppingListItem.OnItemClickEvent += CheckSign;
    }

    private void CheckSign(Item itemToHighlight, ItemGrabArea itemGrabArea) {
        if (itemGrabArea.signToHighlight != null) {
            Debug.Log(itemGrabArea.signToHighlight.gameObject + ", " + gameObject);
            Debug.Log(itemGrabArea.signToHighlight.gameObject.name == gameObject.name);
        }
        if (itemGrabArea.signToHighlight != null && itemGrabArea.signToHighlight.gameObject.name == gameObject.name) {//if (Item.Equals(item, itemToHighlight)) {
            Highlight();
        } else {
            UnHighlight();
        }
    }

    private void Highlight() {
        Material[] list = new Material[1];
        list[0] = highlightMaterial;
        meshRenderer.materials = list;
    }

    private void UnHighlight() {
        Material[] list = new Material[1];
        list[0] = originalMaterial;
        meshRenderer.materials = list;
    }
}
