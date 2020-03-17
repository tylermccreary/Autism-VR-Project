using System.Collections;
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
    [SerializeField] private GameObject trolley;
    [SerializeField] private Material wallLineMaterial;
    [SerializeField] private Material teleportLineMaterial;
    [SerializeField] private GameObject teleportSphereSpotter;

    private bool hasCart = false;
    private RaycastHit hit;

    private bool teleport = false;
    private bool blackIn = false;
    private bool blackOut = false;
    private float blackOutTime = 0.0f;
    private float blackInTime = 0.0f;
    private float blackOutTimeNeeded = 0.1f;

    private bool canTeleport = false;


    public delegate void TeleportEvent();
    public static event TeleportEvent OnTeleport;
    public delegate void PrepareForTeleportEvent();
    public static event PrepareForTeleportEvent OnPrepareForTeleport;
    public delegate void HoverCheckoutEvent(bool hovering);
    public static event HoverCheckoutEvent OnHoverCheckout;

    private void Start() {
        lineRenderer.enabled = false;
    }

    private void OnEnable() {
        ViveInputController.OnButtonPressDownRightTouchPad += ShowTeleportAbility;
        ViveInputController.OnButtonPressUpRightTouchPad += TriggerTeleport;
        GrabRelease.OnCartGrab += EnableCart;
    }

    private void OnDisable() {
        ViveInputController.OnButtonPressDownRightTouchPad -= ShowTeleportAbility;
        ViveInputController.OnButtonPressUpRightTouchPad -= TriggerTeleport;
        GrabRelease.OnCartGrab -= EnableCart;
    }

    private void Update() {
        if (hasCart) {
            UpdateTeleportLineRenderer();
            UpdateBlackOut();
        }
    }

    private void ShowTeleportAbility() {
        teleport = true;
    }

    private void UpdateTeleportLineRenderer() {
        if (teleport) {
            Ray ray = new Ray(rightController.transform.position, -rightController.transform.up);
            if (Physics.Raycast(ray, out hit, 50, layerMask)) {
                if (hit.transform != null) {
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, rightController.transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                    teleportSphereSpotter.transform.position = hit.point;
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer(Layer.TELEPORT_WALL)) {
                        lineRenderer.material = wallLineMaterial;
                        canTeleport = false;
                        teleportSphereSpotter.SetActive(false);
                    } else {
                        lineRenderer.material = teleportLineMaterial;
                        canTeleport = true;
                        teleportSphereSpotter.SetActive(true);
                    }
                } else {
                    lineRenderer.enabled = false;
                }
                
                OnHoverCheckout(hit.transform.gameObject.layer == LayerMask.NameToLayer(Layer.CHECKOUT));
            } else {
                lineRenderer.enabled = false;
                OnHoverCheckout(false);
            }
        } else {
            OnHoverCheckout(false);
        }
    }

    private void TriggerTeleport() {
        teleportSphereSpotter.SetActive(false);
        if (hasCart && canTeleport) {
            lineRenderer.enabled = false;
            teleport = false;
            blackOut = true;
            OnPrepareForTeleport();
            StartCoroutine(ExecuteTeleportation());
        } else {
            lineRenderer.enabled = false;
            teleport = false;
        }
    }

    private IEnumerator ExecuteTeleportation() {
        yield return new WaitForSeconds(blackOutTimeNeeded);
        blackOut = false;
        blackOutTime = 0.0f;
        blackIn = true;
        if (hit.transform != null) {
            transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            OnTeleport();
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

    private void EnableCart() {
        trolley.SetActive(true);
        hasCart = true;
    }
}
