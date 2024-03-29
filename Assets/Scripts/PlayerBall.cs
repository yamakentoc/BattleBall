﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour {

    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] CameraController cameraController;
    private Vector3 previousScale;
    private Vector2 startPos, nowPos, differenceDisVector2;
    private float speed, radian, doubleTapTime, speedUpTime;
    private bool isSpeedUp, isDoubleTapStart;

    void Start() {
        previousScale = transform.localScale;
    }

    void Update() {
        MoveControll();
    }

    void FixedUpdate() {
        Move();     
    }

    void MoveControll() {
        doubleTapTime += Time.deltaTime;
        //Debug.Log(doubleTapTime);
       List< (TouchType touchType, int id)> touchInfos = TouchManager.GetTouchInfo();
        foreach ((TouchType touchType, int id) in touchInfos) {
            switch (touchType) {
                case TouchType.Began:
                    if (id == 0) { startPos = TouchManager.GetTouchPosition(); }
                    if (doubleTapTime <= 0.2f && !isSpeedUp) {
                        Debug.Log("ダブルタップ！ " + doubleTapTime);
                        isSpeedUp = true;
                    }
                    doubleTapTime = 0;
                    break;
                case TouchType.Moved:
                    if (id == 0) {
                        nowPos = TouchManager.GetTouchPosition();
                        differenceDisVector2 = nowPos - startPos;
                        speed = differenceDisVector2 == new Vector2(0, 0) ? 0 : 20;
                        radian = differenceDisVector2 == new Vector2(0, 0) ? radian : Mathf.Atan2(differenceDisVector2.x, differenceDisVector2.y) * Mathf.Rad2Deg;
                    }
                    break;
                case TouchType.Ended:
                    if (id == 0) { speed = 0; }
                    break;
            }
            SpeedUp(touchType, id);
        }
    }


    void SpeedUp(TouchType phase, int id) {
        if (isSpeedUp) {
            speedUpTime += Time.deltaTime;
            if (speedUpTime <= 2.0f) {
                speed = 40;
            } else {
                speedUpTime = 0;
                isSpeedUp = false;
                speed *= 0.5f;
                if (id == 0 && phase == TouchType.None) {
                    speed = 0;
                }
            }
        }
    }

    void Move() {
        rigidbody.AddForce(5 * (transform.forward * speed - rigidbody.velocity));
        rigidbody.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, radian, 0), 1);
       // Debug.Log("velocity.magnitude: " + rigidbody.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("NeutralBall")) {
            transform.localScale = new Vector3(transform.localScale.x + 0.05f,
                                               transform.localScale.y + 0.05f,
                                               transform.localScale.z + 0.05f);

            //デバッグ
            //transform.localScale = new Vector3(transform.localScale.x * 1.1f,
            //                                   transform.localScale.y * 1.1f,
            //                                   transform.localScale.z * 1.1f);

            transform.localPosition = new Vector3(transform.localPosition.x,
                                              transform.localScale.y / 2,
                                              transform.localPosition.z);

            rigidbody.mass = transform.localScale.x;
            cameraController.SetMovePosition((transform.localScale - previousScale) * 2.0f);
            Destroy(other.gameObject);
            previousScale = transform.localScale;
        }
    }
}
