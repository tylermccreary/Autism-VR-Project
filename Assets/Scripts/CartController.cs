using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    [SerializeField] private Transform XRRigTransform;
    [SerializeField] private Transform headTransform;
    private float cartOffset = 0.6f;

    private void OnEnable() {
        Teleport.OnTeleport += MoveCart;
    }

    private void OnDisable() {
        Teleport.OnTeleport -= MoveCart;
    }

    private void MoveCart() {
        bool facingForward = false;
        if (headTransform.rotation.eulerAngles.y > 270 || headTransform.rotation.eulerAngles.y < 90) {
            facingForward = true;
        }

        if (facingForward) {
            MoveCartInFront();
        } else {
            MoveCartBehind();
        }
    }

    private void MoveCartInFront() {
        transform.position = new Vector3(headTransform.position.x, XRRigTransform.position.y, headTransform.position.z) + new Vector3(0, 0, cartOffset);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void MoveCartBehind() {
        transform.position = new Vector3(headTransform.position.x, XRRigTransform.position.y, headTransform.position.z) - new Vector3(0, 0, cartOffset);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
