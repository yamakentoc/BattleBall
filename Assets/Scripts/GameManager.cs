using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GodTouches;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject neutralBall, neutralBalls;

    void Start() {
        int value = 0;
        while (value < 1000) {
            value++;
            float randomX = Random.Range(-150f, 150f);
            float randomZ = Random.Range(-150f, 150f);
            Vector3 position = new Vector3(randomX, neutralBall.transform.localScale.y / 2, randomZ);
            Instantiate(neutralBall, position, Quaternion.identity, neutralBalls.transform);
        }
    }

}
