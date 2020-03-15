using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutExitController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Animator animator;

    private void Start() {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnEnable() {
        Teleport.OnHoverCheckout += CheckHovering;
    }

    private void OnDisable() {
        Teleport.OnHoverCheckout -= CheckHovering;
    }

    private void CheckHovering(bool hovering) {
        if (hovering) {
            animator.StopPlayback();
            Material[] list = new Material[1];
            list[0] = highlightMaterial;
            meshRenderer.materials = list;
        } else {
            animator.StartPlayback();
            Material[] list = new Material[1];
            list[0] = originalMaterial;
            meshRenderer.materials = list;
        }
    }
}
