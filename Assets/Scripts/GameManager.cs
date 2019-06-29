using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject neutralBall, neutralBalls;

    void Start() {
        int value = 0;
        while (value < 1000) {
            value++;
            Vector2 randomCirclePosition = Random.insideUnitCircle * 150f;
            Vector3 position = new Vector3(randomCirclePosition.x, neutralBall.transform.localScale.y / 2, randomCirclePosition.y);
            Instantiate(neutralBall, position, Quaternion.identity, neutralBalls.transform);
        }
    }

}
