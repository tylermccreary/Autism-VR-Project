using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public ViveInputController viveInputController;
    public Transform XRRig;
    public LineRenderer lineRenderer;
    public LayerMask layerMask;
    public GameObject rightController;

    private bool teleport = false;
    private RaycastHit hit;

    private void Start() {
        lineRenderer.gameObject.SetActive(false);
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
        UpdateTeleport();
    }

    private void ShowTeleportAbility() {
        teleport = true;
    }

    private void UpdateTeleport() {
        if (teleport) {
            Ray ray = new Ray(rightController.transform.position, -rightController.transform.up);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.rigidbody != null) {
                    lineRenderer.gameObject.SetActive(true);
                    lineRenderer.SetPosition(0, rightController.transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                } else {
                    lineRenderer.gameObject.SetActive(false);
                }
            } else {
                lineRenderer.gameObject.SetActive(false);
            }
        }
    }

    private void TriggerTeleport() {
        lineRenderer.gameObject.SetActive(false);
        if (hit.rigidbody != null) {
            transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
        teleport = false;
    }
}
