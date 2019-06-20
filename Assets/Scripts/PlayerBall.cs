using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GodTouches;

public class PlayerBall : MonoBehaviour {

    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] CameraController cameraController;
    private Vector3 previousScale;
    private Vector2 startPos, nowPos, differenceDisVector2;
    private float speed, radian, doubleTapTime, speedUpTime;
    private bool isSpeedUp;

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
        GodPhase phase = GodTouch.GetPhase();
        switch (phase) {
            case GodPhase.Began:
                startPos = GodTouch.GetPosition();
                if (doubleTapTime <= 0.2f && !isSpeedUp) {
                    //ダブルタップしたら何秒かダブルタップできないようにする必要がある
                    Debug.Log("ダブルタップ！ " + doubleTapTime);
                    isSpeedUp = true;
                }
                doubleTapTime = 0;
                break;
            case GodPhase.Moved:
                nowPos = GodTouch.GetPosition();
                differenceDisVector2 = nowPos - startPos;
                speed = differenceDisVector2 == new Vector2(0, 0) ? 0 : 20;
                radian = differenceDisVector2 == new Vector2(0, 0) ? radian : Mathf.Atan2(differenceDisVector2.x, differenceDisVector2.y) * Mathf.Rad2Deg;
                break;
            case GodPhase.Ended:
                speed = 0;
                break;
        }
        SpeedUp(phase);
        Debug.Log(rigidbody.velocity.magnitude);
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

    void SpeedUp(GodPhase phase) {
        if (isSpeedUp) {
            speedUpTime += Time.deltaTime;
            if (speedUpTime <= 2.0f) {
                if (phase == GodPhase.None) {
                    speed = 20;
                }
                speed *= 2;
            } else {
                speedUpTime = 0;
                isSpeedUp = false;
                speed *= 0.5f;
                if (phase == GodPhase.None) {
                    speed = 0;
                }
            }
        }
    }
}
