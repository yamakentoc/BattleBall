using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour {

    private Rigidbody rigidbody;
    [SerializeField] CameraController cameraController;
    [SerializeField] ParticleSystem particle;
    [SerializeField] NamePlate namePlate;
    private Vector3 previousScale;
    private Vector2 startPos, nowPos, differenceDisVector2;
    private float speed, radian, doubleTapTime, speedUpTime;
    private bool isSpeedUp, isDoubleTapStart, isContinueMovefromDoblueTap;

    void Start() {
        previousScale = transform.localScale;
        rigidbody = GetComponent<Rigidbody>();
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
                        if (!(differenceDisVector2.x < 10 && differenceDisVector2.x > -10 && differenceDisVector2.y < 10 && differenceDisVector2.y > -10)) {
                            speed = differenceDisVector2 == new Vector2(0, 0) ? 0 : 20;
                            if (isContinueMovefromDoblueTap) { speed = 20; }
                            radian = differenceDisVector2 == new Vector2(0, 0) ? radian : Mathf.Atan2(differenceDisVector2.x, differenceDisVector2.y) * Mathf.Rad2Deg;
                        }
                    }
                    break;
                case TouchType.Ended:
                    if (id == 0) { speed = 0; }
                    isContinueMovefromDoblueTap = false;
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
                speed = 20;
                if (id == 0 && phase == TouchType.None) { speed = 0; }
                if (phase == TouchType.Stationary) {
                    isContinueMovefromDoblueTap = true;
                }
            }
        }
    }

    void Move() {
        rigidbody.AddForce(5 * (transform.forward * speed - rigidbody.velocity));
        //Debug.Log(radian);
        rigidbody.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, radian, 0), 1);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("NeutralBall")) {
            transform.localScale = new Vector3(transform.localScale.x + 0.05f,
                                               transform.localScale.y + 0.05f,
                                               transform.localScale.z + 0.05f);
            transform.localPosition = new Vector3(transform.localPosition.x,
                                              transform.localScale.y / 2,
                                              transform.localPosition.z);

            rigidbody.mass = transform.localScale.x / 3.0f;
            Vector3 diffScale = transform.localScale - previousScale;
            namePlate.ChangePosition(diffScale);
            particle.transform.localScale += diffScale / 3.0f;
            particle.startLifetime += diffScale.x / 4.0f;
            cameraController.SetMovePosition(diffScale * 2.0f);
            Destroy(other.gameObject);
            previousScale = transform.localScale;
        }
    }
}
