using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Camera camera;
    [SerializeField] GameObject playerBall;
    
    private Vector3 offset, hoge;

    void Start() {
        offset = transform.position - playerBall.transform.position;       
    }

    void LateUpdate() {
        transform.localPosition = playerBall.transform.position + offset + hoge;
    }

    public void SetHoge(Vector3 hoge) {
        this.hoge += new Vector3(0, hoge.y, -hoge.z);
        Debug.Log("hoge: " + hoge + "this.hoge: " + this.hoge);
    }

}