using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartUIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void OnEnable() {
        GrabRelease.OnCartGrab += HideCartUI;
    }

    private void OnDisable() {
        GrabRelease.OnCartGrab -= HideCartUI;
    }

    private void HideCartUI() {
        canvas.enabled = false;
    }
}
