using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {
    private static Vector3 TouchPosition = Vector3.zero;

    public static TouchType GetTouchType() {
        if (Application.isEditor) {
            if (Input.GetMouseButtonDown(0)) { return TouchType.Began; }
            if (Input.GetMouseButton(0)) { return TouchType.Moved; }
            if (Input.GetMouseButtonUp(0)) { return TouchType.Ended; }
        } else {
            if (Input.touchCount > 0) {
                return (TouchType)((int)Input.GetTouch(0).phase);
            }
        }
        return TouchType.None;
    }

    public static Vector3 GetTouchPosition() {
        if (Application.isEditor) {
            TouchType touch = GetTouchType();
            if (touch != TouchType.None) { return Input.mousePosition; }
        } else {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                TouchPosition.x = touch.position.x;
                TouchPosition.y = touch.position.y;
                return TouchPosition;
            }
        }
        return Vector3.zero;
    }

    public static Vector3 GetTouchWorldPosition(Camera camera) {
        return camera.ScreenToWorldPoint(GetTouchPosition());
    }
}

public enum TouchType {
    None = 99,
    Began = 0,
    Moved = 1,
    Stationary = 2,
    Ended = 3,
    Canceld = 4,
}

