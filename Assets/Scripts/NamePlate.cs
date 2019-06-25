using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NamePlate : MonoBehaviour {
    [SerializeField] GameObject ball;
    private Vector3 offset, ballDiffScale;
    private TextMeshPro textMeshPro;
    private int ballValue;

    void Start() {
        offset = transform.position - ball.transform.position;
        textMeshPro = GetComponent<TextMeshPro>();
        UpdatePosition();
    }

    void LateUpdate() {
        UpdatePosition();
    }

    void UpdatePosition() {
        Vector3 playerBallPos = ball.transform.localPosition;
        transform.localPosition = playerBallPos + offset + ballDiffScale;
    }

    public void ChangePosition(Vector3 diff) {
        ballDiffScale += new Vector3(0, diff.y / 2.0f, 0);
        textMeshPro.fontSize += diff.x * 2;
        ballValue += 1;
        textMeshPro.text = "Player\n" + ballValue;
    }

}