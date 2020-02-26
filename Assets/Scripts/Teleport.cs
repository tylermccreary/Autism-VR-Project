﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Image blackoutCurtain;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject rightController;
    [SerializeField] private Color blackOutColor;
    [SerializeField] private Color transparentColor;


    private RaycastHit hit;

    private bool teleport = false;
    private bool blackIn = false;
    private bool blackOut = false;
    private float blackOutTime = 0.0f;
    private float blackInTime = 0.0f;
    private float blackOutTimeNeeded = 0.1f;

    private void Start() {
        lineRenderer.enabled = false;
    }

    private void OnEnable() {
        ViveInputController.OnButtonPressDownRightTouchPad += ShowTeleportAbility;
        ViveInputController.OnButtonPressUpRightTouchPad += TriggerTeleport;
    }

    private void OnDisable() {
        ViveInputController.OnButtonPressDownRightTouchPad -= ShowTeleportAbility;
        ViveInputController.OnButtonPressUpRightTouchPad -= TriggerTeleport;
    }

    private void Update() {
        UpdateTeleportLineRenderer();
        UpdateBlackOut();
    }

    private void ShowTeleportAbility() {
        teleport = true;
    }

    private void UpdateTeleportLineRenderer() {
        if (teleport) {
            Ray ray = new Ray(rightController.transform.position, -rightController.transform.up);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.rigidbody != null) {
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, rightController.transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                } else {
                    lineRenderer.enabled = false;
                }
            } else {
                lineRenderer.enabled = false;
            }
        }
    }

    private void TriggerTeleport() {
        lineRenderer.enabled = false;
        teleport = false;
        blackOut = true;
        StartCoroutine(ExecuteTeleportation());
    }

    private IEnumerator ExecuteTeleportation() {
        yield return new WaitForSeconds(blackOutTimeNeeded);
        blackOut = false;
        blackOutTime = 0.0f;
        blackIn = true;
        if (hit.rigidbody != null) {
            transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }

    private void UpdateBlackOut() {
        if (blackOut) {
            blackOutTime += Time.deltaTime;
            blackoutCurtain.color = Color.Lerp(transparentColor, blackOutColor, blackOutTime / blackOutTimeNeeded);
            if (blackOutTime > blackOutTimeNeeded) {
                blackOut = false;
                blackOutTime = 0.0f;
            }
        }
        if (blackIn) {
            blackInTime += Time.deltaTime;
            blackoutCurtain.color = Color.Lerp(blackOutColor, transparentColor, blackInTime / blackOutTimeNeeded);
            if (blackInTime > blackOutTimeNeeded) {
                blackIn = false;
                blackInTime = 0.0f;
            }
        }
    }
}
