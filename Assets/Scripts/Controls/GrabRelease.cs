﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRelease : MonoBehaviour {

    private enum HandEnum { Left, Right };
    [SerializeField] private HandEnum hand;
    [SerializeField] private MeshRenderer[] meshRenderers;

    private GameObject itemToGrab;
    private GameObject itemInHand;
    private Rigidbody itemInHandRigid;
    private bool itemIsCart = false;

    private ItemGrabArea itemArea;
    private Vector3 prevPosition = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    
    public delegate void CartGrabEvent();
    public static event CartGrabEvent OnCartGrab;

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

    private void Update() {
        velocity = (transform.position - prevPosition) / Time.deltaTime;
        prevPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        itemIsCart = false;
        if (other.tag == Tag.ITEM) {
            if (itemToGrab == null && itemInHand == null) {
                itemToGrab = other.gameObject;
            }
        } else if (other.tag == Tag.CART_AREA) {
            itemIsCart = true;
        } else {
            itemArea = other.gameObject.GetComponent<ItemGrabArea>();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == itemToGrab) {
            itemToGrab = null;
        }
        ItemGrabArea itemAreaExited = other.gameObject.GetComponent<ItemGrabArea>();
        if (itemAreaExited == itemArea) {
            itemArea = null;
        }
    }

    private void Grab() {
        if (itemIsCart) {
            OnCartGrab();
        } if (itemToGrab != null) {
            itemInHand = itemToGrab;
            itemInHandRigid = itemInHand.GetComponent<Rigidbody>();
            itemInHandRigid.isKinematic = true;
            itemInHand.transform.parent = transform;
        } else {
            if (itemArea != null) {
                GameObject newItem = Instantiate(itemArea.prefabToSpawn, transform);
                itemInHand = newItem;
                itemInHandRigid = itemInHand.GetComponent<Rigidbody>();
                itemInHandRigid.isKinematic = true;
                //itemInHand.transform.parent = transform;
                foreach(MeshRenderer renderer in meshRenderers) {
                    renderer.enabled = false;
                }
            }
        }
    }

    private void Release() {
        if (itemInHand != null) {
            if (itemArea != null && itemInHand.name.Contains(itemArea.prefabToSpawn.name)) {
                Debug.Log("Destroy");
                Destroy(itemInHand);
            } else {
                itemInHandRigid.isKinematic = false;
                itemInHand.transform.parent = null;
                Debug.Log(velocity);
                itemInHandRigid.velocity = velocity * 2;
                itemInHand = null;
            }

            foreach (MeshRenderer renderer in meshRenderers) {
                renderer.enabled = true;
            }
        }
    }
}
