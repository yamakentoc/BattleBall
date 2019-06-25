using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour {
    [SerializeField] GameObject playerBall;
    private Vector3 offset, playerBallDiffScale;
    private TextMeshPro textMeshPro;
    private int ballValue;

    void Start() {
        offset = transform.position - playerBall.transform.position;
        textMeshPro = GetComponent<TextMeshPro>();
        UpdatePosition();
    }

    void LateUpdate() {
        UpdatePosition();
    }

    void UpdatePosition() {
        Vector3 playerBallPos = playerBall.transform.localPosition;
        transform.localPosition = playerBallPos + offset + playerBallDiffScale;
    }

    public void ChangePosition(Vector3 diff) { 
        playerBallDiffScale += new Vector3(0, diff.y / 2.0f, 0);
        textMeshPro.fontSize += diff.x * 2;
        ballValue += 1;
        textMeshPro.text = "Player\n" + ballValue;
    }
}
