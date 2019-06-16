using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Camera camera;
    [SerializeField] GameObject playerBall;
    //private SphereCollider sphereCollider;
    private float radius;
    private Vector3 initOffset, offset;



    public float smoothTime = 0.5f;

    public float minZoom = 40;
    public float maxZoom = 10;
    public float zoomLimiter = 50;

    private Vector3 hoge;

    void Start() {
        
        //sphereCollider = playerBall.GetComponent<SphereCollider>();
        offset = transform.position - playerBall.transform.position;
        initOffset = offset;
    }

    void LateUpdate() {
        
        radius = playerBall.transform.localScale.x / 2;
        //Debug.Log(radius);
        float distance = (radius + 10) / Mathf.Sin(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);//カメラの距離
        float frustumHeight = 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);//錐台の高さ
        float frustumWidth = frustumHeight * camera.aspect;
        //Debug.Log("画面の横幅: " + frustumWidth + ", ボールの直径: " + radius * 2);
        Debug.Log("カメラの横幅: " + frustumWidth);
        if (frustumWidth / 2 < radius * 2) {
            Debug.Log("イアまだだ");
            var direction = playerBall.transform.localRotation * Vector3.forward;
            camera.fieldOfView++;
            
        }
        //camera.fieldOfView = 2.0f * Mathf.Atan(frustumHeight * 0.5f / distance) * Mathf.Rad2Deg;



        //float distance = GetCameraDistanceWithWidthSize(camera, playerBall.transform.localScale.x + 2);

        //offset = new Vector3(offset.x, initOffset.y + distance, initOffset.z - distance);
        //camera.fieldOfView = 10 + distance;
        //Debug.Log(distance);
        transform.localPosition = playerBall.transform.position + offset;
        //camera.transform.LookAt(playerBall.transform);
    }

 

    //public float GetCameraDistanceWithWidthSize(Camera camera, float width) {
    //    if (camera == null || width <= 0.0f) {
    //        return 0.0f;
    //    }
    //    float frustumHeight = width / camera.aspect;
    //    return frustumHeight * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    //}


}