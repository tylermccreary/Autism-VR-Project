using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperBotController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;
    private bool playerInRange = false;
    private bool showingPlayer = false;
    private Transform itemTransform;
    private Item itemShowing;

    private Vector3 originalPosition;

    private void OnEnable() {
        ShoppingListItem.OnItemClickEvent += ShowPlayer;
        CartItemTracker.OnCartContentsAdd += GoBack;
    }

    private void OnDisable() {
        ShoppingListItem.OnItemClickEvent -= ShowPlayer;
        CartItemTracker.OnCartContentsAdd -= GoBack;
    }

    private void Start() {
        originalPosition = transform.position;
    }

    private void Update() {
        if (showingPlayer) {
            if (playerInRange) {
                navAgent.isStopped = false;
            } else {
                navAgent.isStopped = true;
            }
        } else {

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tag.PLAYER) {
            RotateTowardsPlayer();
            playerInRange = true;
        }
    }

    private void RotateTowardsPlayer() {

    }

    private void ShowPlayer(Item item, Transform areaTransform) {
        if (playerInRange) {
            showingPlayer = true;
            itemTransform = areaTransform;
            itemShowing = item;
            navAgent.destination = areaTransform.position;
        }
    }

    private void GoBack(Item item) {
        if (showingPlayer) {
            if (Item.Equals(item, itemShowing)) {
                showingPlayer = false;
            }
        }
    }
}
