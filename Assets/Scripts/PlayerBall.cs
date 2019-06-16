using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GodTouches;

public class PlayerBall : MonoBehaviour {

    [SerializeField] new Rigidbody rigidbody;
    private float speed, radian, differenceDisFloat;
    private Vector2 startPos, nowPos, differenceDisVector2;

    void Start() {
    
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
            Debug.Log(startPos);
        }
        //スワイプしてる時
        if (phase == GodPhase.Moved) {
            nowPos = GodTouch.GetPosition();
            differenceDisVector2 = nowPos - startPos;
            speed = 5;
            radian = Mathf.Atan2(differenceDisVector2.x, differenceDisVector2.y) * Mathf.Rad2Deg;
        }
        //離した時
        if (phase == GodPhase.Ended) {
            speed = 0;
        }
    }

    void Move() {
        rigidbody.velocity = transform.forward * speed;
        rigidbody.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, radian, 0), 10);
    }
}
