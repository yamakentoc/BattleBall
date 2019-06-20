using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {
    private static Vector3 TouchPosition = Vector3.zero;

    public static List<(TouchType touchType, int id)> GetTouchInfo() {
        List<(TouchType touchType, int id)> touchInfos = new List<(TouchType, int)>();
    if (Application.isEditor) {
            if (Input.GetMouseButtonDown(0)) { touchInfos.Add((TouchType.Began, 0)); }
            if (Input.GetMouseButton(0)) { touchInfos.Add((TouchType.Moved, 0)); }
            if (Input.GetMouseButtonUp(0)) { touchInfos.Add((TouchType.Ended, 0)); }
            if (touchInfos.Count == 0) { touchInfos.Add((TouchType.None, 0)); }
            return touchInfos;
        } else {
            if (Input.touchCount > 0) {
                foreach(Touch touch in Input.touches) {
                    touchInfos.Add(((TouchType)((int)touch.phase), touch.fingerId));
                    //Debug.Log("指のID: " + touch.fingerId);
                }
                return touchInfos;
            }
        }
        return new List<(TouchType touchType, int id)> { (TouchType.None, 0) };
        
    }

    public static Vector3 GetTouchPosition() {
        if (Application.isEditor) {
            TouchType touch = GetTouchInfo()[0].touchType;
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

