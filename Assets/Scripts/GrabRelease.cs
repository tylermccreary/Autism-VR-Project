using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRelease : MonoBehaviour {

    private enum HandEnum { Left, Right };
    [SerializeField] private HandEnum hand;

    private GameObject itemToGrab;
    private GameObject itemInHand;
    private Rigidbody itemInHandRigid;

    private void OnEnable() {
        if (hand == HandEnum.Right) {
            ViveInputController.OnButtonDownRightTrigger += Grab;
            ViveInputController.OnButtonUpRightTrigger += Release;
        } else if (hand == HandEnum.Left) {
            ViveInputController.OnButtonDownLeftTrigger += Grab;
            ViveInputController.OnButtonUpLeftTrigger += Release;
        }
    }

    private void OnDisable() {
        if (hand == HandEnum.Right) {
            ViveInputController.OnButtonDownRightTrigger -= Grab;
            ViveInputController.OnButtonUpRightTrigger -= Release;
        } else if (hand == HandEnum.Left) {
            ViveInputController.OnButtonDownLeftTrigger -= Grab;
            ViveInputController.OnButtonUpLeftTrigger -= Release;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tag.ITEM) {
            if (itemToGrab == null && itemInHand == null) {
                itemToGrab = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == itemToGrab) {
            itemToGrab = null;
        }
    }

    private void Grab() {
        if (itemToGrab != null) {
            itemInHand = itemToGrab;
            itemInHandRigid = itemInHand.GetComponent<Rigidbody>();
            itemInHandRigid.isKinematic = true;
            itemInHand.transform.parent = transform;
        }
    }

    private void Release() {
        if (itemInHand != null) {
            itemInHandRigid.isKinematic = false;
            itemInHand.transform.parent = null;
            itemInHand = null;
        }
    }
}
