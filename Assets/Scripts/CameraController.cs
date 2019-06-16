using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] GameObject playerBall;
    private Vector3 offset;

    void Start() {
        offset = transform.position - playerBall.transform.position;
    }

    void LateUpdate() {
        transform.position = playerBall.transform.position + offset;
    }
}
