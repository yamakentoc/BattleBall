using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour {

    [SerializeField] GameObject playerBall, cameraManager;
    private Vector3 offset, movePosition;

    void Start() {
        offset = transform.position - playerBall.transform.position;       
    }

    void LateUpdate() {
        transform.localPosition = playerBall.transform.position + offset;
    }

    public void SetMovePosition(Vector3 movePosition) {
        this.movePosition += new Vector3(0, movePosition.y, -movePosition.z);
        cameraManager.transform.DOMove(this.movePosition, 0.7f);
    }

}