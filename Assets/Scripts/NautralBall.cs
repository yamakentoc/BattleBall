using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NautralBall : MonoBehaviour {
    private float speed = 30.0f;
    public bool dropDown = false;

    private void FixedUpdate() {
        if (!dropDown) { return; }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.375f, transform.position.z), step);
    }
}
