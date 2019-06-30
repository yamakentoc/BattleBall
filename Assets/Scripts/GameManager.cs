using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject player, enemy;
    [SerializeField] GameObject neutralBall, neutralBalls;
    private const int SUM_PLAYER = 8;

    void Start() {
        DeployPlayerBall();
        DeployNeutralBall();
    }

    private void DeployNeutralBall() {
        int value = 0;
        while (value < 1000) {
            value++;
            Vector2 randomCirclePosition = Random.insideUnitCircle * 150f;
            Vector3 position = new Vector3(randomCirclePosition.x, neutralBall.transform.localScale.y / 2, randomCirclePosition.y);
            Instantiate(neutralBall, position, Quaternion.identity, neutralBalls.transform);
        }
    }

    private void DeployPlayerBall() {
        int playerPosition = Random.Range(0, 8);
        for (int i = 0; i < SUM_PLAYER; i++) {
            Vector3 position = Quaternion.Euler(0f, 360f / SUM_PLAYER * i, 0f) * transform.forward * 75f;
            if (i == playerPosition) {
                player.transform.position = position;
            } else {
                GameObject enemyBall = Instantiate(enemy, position, Quaternion.identity);
            }
        }
    }


}
