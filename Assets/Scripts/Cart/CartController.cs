using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    [SerializeField] private Transform XRRigTransform;
    [SerializeField] private Transform headTransform;
    [SerializeField] private CartItemTracker cartItemTracker;
    private float cartOffset = 1.2f;

    private void OnEnable() {
        Teleport.OnTeleport += MoveCart;
        Teleport.OnPrepareForTeleport += MakeItemsChildren;
    }

    private void OnDisable() {
        Teleport.OnTeleport -= MoveCart;
        Teleport.OnPrepareForTeleport -= MakeItemsChildren;
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

        MakeItemsIndependent();
    }

    private void MoveCartInFront() {
        transform.position = new Vector3(headTransform.position.x, XRRigTransform.position.y, headTransform.position.z) + new Vector3(0, 0, cartOffset);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void MoveCartBehind() {
        transform.position = new Vector3(headTransform.position.x, XRRigTransform.position.y, headTransform.position.z) - new Vector3(0, 0, cartOffset);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void MakeItemsChildren() {
        List<Item> itemList = cartItemTracker.ItemsList;
        foreach(Item item in itemList) {
            item.transform.parent = transform;
            Rigidbody rigid = item.gameObject.GetComponent<Rigidbody>();
            rigid.isKinematic = true;
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void MakeItemsIndependent() {
        List<Item> itemList = cartItemTracker.ItemsList;
        foreach (Item item in itemList) {
            item.transform.parent = null;
            //item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
