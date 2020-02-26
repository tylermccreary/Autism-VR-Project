using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveInputController : MonoBehaviour {

    protected static ViveInputController viveInputInstance;

    public static ViveInputController getInstance() {
        return viveInputInstance;
    }

    public delegate void ButtonDownLeftMenu();
    public delegate void ButtonDownRightMenu();
    public delegate void ButtonUpLeftMenu();
    public delegate void ButtonUpRightMenu();
    public delegate void ButtonDownLeftTrigger();
    public delegate void ButtonDownRightTrigger();
    public delegate void ButtonUpLeftTrigger();
    public delegate void ButtonUpRightTrigger();
    public delegate void ButtonDownLeftGrip();
    public delegate void ButtonDownRightGrip();
    public delegate void ButtonUpLeftGrip();
    public delegate void ButtonUpRightGrip();
    public delegate void ButtonPressDownLeftTouchPad();
    public delegate void ButtonPressDownRightTouchPad();
    public delegate void ButtonPressUpLeftTouchPad();
    public delegate void ButtonPressUpRightTouchPad();
    public delegate void ButtonTouchCoordsLeftTouchPad(Vector2 axes);
    public delegate void ButtonTouchCoordsRightTouchPad(Vector2 axes);
    public delegate void ButtonTouchUpLeftTouchPad();
    public delegate void ButtonTouchUpRightTouchPad();

    public static event ButtonDownLeftMenu OnButtonDownLeftMenu;
    public static event ButtonDownRightMenu OnButtonDownRightMenu;
    public static event ButtonUpLeftMenu OnButtonUpLeftMenu;
    public static event ButtonUpRightMenu OnButtonUpRightMenu;
    public static event ButtonDownLeftTrigger OnButtonDownLeftTrigger;
    public static event ButtonDownRightTrigger OnButtonDownRightTrigger;
    public static event ButtonUpLeftTrigger OnButtonUpLeftTrigger;
    public static event ButtonUpRightTrigger OnButtonUpRightTrigger;
    public static event ButtonDownLeftGrip OnButtonDownLeftGrip;
    public static event ButtonDownRightGrip OnButtonDownRightGrip;
    public static event ButtonUpLeftGrip OnButtonUpLeftGrip;
    public static event ButtonUpRightGrip OnButtonUpRightGrip;
    public static event ButtonPressDownLeftTouchPad OnButtonPressDownLeftTouchPad;
    public static event ButtonPressDownRightTouchPad OnButtonPressDownRightTouchPad;
    public static event ButtonPressUpLeftTouchPad OnButtonPressUpLeftTouchPad;
    public static event ButtonPressUpRightTouchPad OnButtonPressUpRightTouchPad;
    public static event ButtonTouchCoordsLeftTouchPad OnButtonTouchCoordsLeftTouchPad;
    public static event ButtonTouchCoordsRightTouchPad OnButtonTouchCoordsRightTouchPad;
    public static event ButtonTouchUpLeftTouchPad OnButtonTouchUpLeftTouchPad;
    public static event ButtonTouchUpRightTouchPad OnButtonTouchUpRightTouchPad;


    private void Awake() {
        if (viveInputInstance == null) {
            viveInputInstance = this;
        } else {
            gameObject.SetActive(false);
            Debug.Log("You have more than one ViveInput in your scene.");
        }
        Debug.Log("ViveInput Awake");
    }

    void Update() {
        CheckControllerButtonEvents();
    }

    private void CheckControllerButtonEvents() {
        UpdateLeftControllerMenuButtonDownEvent();
        UpdateLeftControllerMenuButtonUpEvent();
        UpdateLeftControllerTriggerDownEvent();
        UpdateLeftControllerTriggerUpEvent();
        UpdateLeftControllerGripEvent();
        UpdateLeftControllerTouchPadPressDownEvent();
        UpdateLeftControllerTouchPadPressUpEvent();
        UpdateLeftControllerTouchPadTouchEventWithCoords();
        UpdateLeftControllerTouchPadTouchUpEvent();

        UpdateRightControllerMenuButtonDownEvent();
        UpdateRightControllerMenuButtonUpEvent();
        UpdateRightControllerTriggerDownEvent();
        UpdateRightControllerTriggerUpEvent();
        UpdateRightControllerGripEvent();
        UpdateRightControllerTouchPadPressDownEvent();
        UpdateRightControllerTouchPadPressUpEvent();
        UpdateRightControllerTouchPadTouchEventWithCoords();
        UpdateRightControllerTouchPadTouchUpEvent();
    }

    private void UpdateLeftControllerMenuButtonDownEvent() {
        if (Input.GetButtonDown("LeftControllerMenuButton")) {
            if (OnButtonDownLeftMenu != null) {
                OnButtonDownLeftMenu();
            }
        }
    }

    private void UpdateRightControllerMenuButtonDownEvent() {
        if (Input.GetButtonDown("RightControllerMenuButton")) {
            if (OnButtonDownRightMenu != null) {
                OnButtonDownRightMenu();
            }
        }
    }

    private void UpdateLeftControllerMenuButtonUpEvent() {
        if (Input.GetButtonUp("LeftControllerMenuButton")) {
            if (OnButtonUpLeftMenu != null) {
                OnButtonUpLeftMenu();
            }
        }
    }

    private void UpdateRightControllerMenuButtonUpEvent() {
        if (Input.GetButtonUp("RightControllerMenuButton")) {
            if (OnButtonUpRightMenu != null) {
                OnButtonUpRightMenu();
            }
        }
    }

    private void UpdateLeftControllerTriggerDownEvent() {
        if (Input.GetButtonDown("LeftControllerTrigger")) {
            if (OnButtonDownLeftTrigger != null) {
                OnButtonDownLeftTrigger();
            }
        }
    }

    private void UpdateRightControllerTriggerDownEvent() {
        if (Input.GetButtonDown("RightControllerTrigger")) {
            if (OnButtonDownRightTrigger != null) {
                OnButtonDownRightTrigger();
            }
        }
    }

    private void UpdateLeftControllerTriggerUpEvent() {
        if (Input.GetButtonUp("LeftControllerTrigger")) {
            if (OnButtonUpLeftTrigger != null) {
                OnButtonUpLeftTrigger();
            }
        }
    }

    private void UpdateRightControllerTriggerUpEvent() {
        if (Input.GetButtonUp("RightControllerTrigger")) {
            if (OnButtonUpRightTrigger != null) {
                OnButtonUpRightTrigger();
            }
        }
    }

    private void UpdateLeftControllerGripEvent() {
        if (Input.GetAxis("LeftControllerGripButton") > 0) {
            if (OnButtonDownLeftGrip != null) {
                OnButtonDownLeftGrip();
            }
        } else {
            if (OnButtonUpLeftGrip != null) {
                OnButtonUpLeftGrip();
            }
        }
    }

    private void UpdateRightControllerGripEvent() {
        if (Input.GetAxis("RightControllerGripButton") > 0) {
            if (OnButtonDownRightGrip != null) {
                OnButtonDownRightGrip();
            }
        } else {
            if (OnButtonUpRightGrip != null) {
                OnButtonUpRightGrip();
            }
        }
    }

    private void UpdateLeftControllerTouchPadPressDownEvent() {
        if (Input.GetButtonDown("LeftControllerTrackpadPress")) {
            if (OnButtonPressDownLeftTouchPad != null) {
                OnButtonPressDownLeftTouchPad();
            }
        }
    }

    private void UpdateRightControllerTouchPadPressDownEvent() {
        if (Input.GetButtonDown("RightControllerTrackpadPress")) {
            if (OnButtonPressDownRightTouchPad != null) {
                OnButtonPressDownRightTouchPad();
            }
        }
    }

    private void UpdateLeftControllerTouchPadPressUpEvent() {
        if (Input.GetButtonUp("LeftControllerTrackpadPress")) {
            if (OnButtonPressUpLeftTouchPad != null) {
                OnButtonPressUpLeftTouchPad();
            }
        }
    }

    private void UpdateRightControllerTouchPadPressUpEvent() {
        if (Input.GetButtonUp("RightControllerTrackpadPress")) {
            if (OnButtonTouchUpRightTouchPad != null) {
                OnButtonTouchUpRightTouchPad();
            }
        }
    }

    private void UpdateLeftControllerTouchPadTouchEventWithCoords() {
        if (Input.GetButton("LeftControllerTrackpadTouch")) {
            if (OnButtonTouchCoordsLeftTouchPad != null) {
                float x = -Input.GetAxis("LeftControllerTrackPadHorizontal");
                float y = -Input.GetAxis("LeftControllerTrackPadVertical");
                OnButtonTouchCoordsLeftTouchPad(new Vector2(x, y));
            }
        }
    }

    private void UpdateRightControllerTouchPadTouchEventWithCoords() {
        if (Input.GetButton("RightControllerTrackpadTouch")) {
            if (OnButtonTouchCoordsRightTouchPad != null) {
                float x = -Input.GetAxis("RightControllerTrackPadHorizontal");
                float y = -Input.GetAxis("RightControllerTrackPadVertical");
                OnButtonTouchCoordsRightTouchPad(new Vector2(x, y));
            }
        }
    }

    private void UpdateLeftControllerTouchPadTouchUpEvent() {
        if (Input.GetButtonUp("LeftControllerTrackpadTouch")) {
            if (OnButtonTouchUpLeftTouchPad != null) {
                OnButtonTouchUpLeftTouchPad();
            }
        }
    }

    private void UpdateRightControllerTouchPadTouchUpEvent() {
        if (Input.GetButtonUp("RightControllerTrackpadTouch")) {
            if (OnButtonTouchUpRightTouchPad != null) {
                OnButtonTouchUpRightTouchPad();
            }
        }
    }
}