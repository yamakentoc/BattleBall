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
        Debug.Log(rigidbody.velocity.magnitude);
        //rigidbody.velocity = transform.forward * speed;
        //rigidbody.AddForce(transform.forward * speed, ForceMode.Force);
        rigidbody.AddForce(5 * (transform.forward * speed - rigidbody.velocity));
        rigidbody.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, radian, 0), 10);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("NeutralBall")) {
            transform.localScale = transform.localScale * 1.1f;
            transform.localPosition = new Vector3(transform.localPosition.x,
                                              transform.localScale.y / 2,
                                              transform.localPosition.z);
            Destroy(other.gameObject);
        }
    }
}