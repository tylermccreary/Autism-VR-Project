using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabArea : MonoBehaviour
{
    public GameObject prefabToSpawn;
    [SerializeField] public Item item;
    public GameObject signToHighlight;

    private Material originalMaterial;
    [SerializeField] private Material highlightMaterial;
    private MeshRenderer meshRenderer;

    public void OnEnable() {
        ShoppingListItem.OnItemClickEvent += CheckSign;
    }

    private void CheckSign(Item itemToHighlight) {
        if (Item.Equals(item, itemToHighlight)) {
            Highlight();
        } else {
            UnHighlight();
        }
    }

    private void Highlight() {
        Debug.Log("Here");
        Material[] list = new Material[1];
        list[0] = highlightMaterial;
        meshRenderer.materials = list;
    }

    private void UnHighlight() {
        if (gameObject.name.Contains("Cabbage")) {
            Debug.Log("UNDONE");
        }
        meshRenderer.materials[0] = originalMaterial;
    }

    private void Start() {
        meshRenderer = signToHighlight.GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.materials[0];
    }
}
