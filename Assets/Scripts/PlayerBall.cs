﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GodTouches;

public class PlayerBall : MonoBehaviour {

    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] CameraController cameraController;
    private float speed, radian, differenceDisFloat;
    private Vector2 startPos, nowPos, differenceDisVector2;
    private Vector3 previousScale;

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
        GodPhase phase = GodTouch.GetPhase();
        //タッチし始めた時
        if (phase == GodPhase.Began) {
            startPos = GodTouch.GetPosition();
        }
        //スワイプしてる時
        if (phase == GodPhase.Moved) {
            nowPos = GodTouch.GetPosition();
            differenceDisVector2 = nowPos - startPos;
            speed = 20;
            radian = Mathf.Atan2(differenceDisVector2.x, differenceDisVector2.y) * Mathf.Rad2Deg;
        }
        //離した時
        if (phase == GodPhase.Ended) {
            speed = 0;
        }
    }

    void Move() {
        //rigidbody.velocity = transform.forward * speed;
        //rigidbody.AddForce(transform.forward * speed, ForceMode.Force);
        rigidbody.AddForce(5 * (transform.forward * speed - rigidbody.velocity));
        rigidbody.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, radian, 0), 10);
        Debug.Log("velocity.magnitude: " + rigidbody.velocity.magnitude);
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
