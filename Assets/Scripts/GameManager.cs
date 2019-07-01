using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject player, enemyPrefab;
    [SerializeField] GameObject neutralBall, neutralBalls;
    [SerializeField] List<Material> ballMaterials;
    private GameObject playerBall; 
    private const int SUM_PLAYER = 8;
    private GradientColorKey[] colorKey = new GradientColorKey[2];
    private GradientAlphaKey[] alphaKey = new GradientAlphaKey[3];
    private Gradient gradient = new Gradient();

    void Start() {
        playerBall = player.transform.GetChild(0).gameObject;
        SetGradientKey();
        DeployPlayerBall();
        DeployNeutralBall();
    }

    private void SetGradientKey() {
        colorKey[0].time = 0.35f;
        colorKey[1].color = Color.white;
        colorKey[1].time = 1.0f;
        alphaKey[0].alpha = 0.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.15f;
        alphaKey[1].time = 0.338f;
        alphaKey[2].alpha = 0.0f;
        alphaKey[2].time = 1.0f;
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
        ballMaterials = ballMaterials.OrderBy(a => System.Guid.NewGuid()).ToList();
        int playerPosition = Random.Range(0, 8);
        
        for (int i = 0; i < SUM_PLAYER; i++) {
            Vector3 position = Quaternion.Euler(0f, 360f / SUM_PLAYER * i, 0f) * transform.forward * 75f;
            if (i == playerPosition) {
                player.transform.position = position;
                playerBall.GetComponent<Renderer>().material.color = ballMaterials[i].color;
                SetGradient(playerBall, ballMaterials[i].color);
            } else {
                GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                GameObject enemyBall = enemy.transform.GetChild(0).gameObject;
                enemyBall.GetComponent<Renderer>().material.color = ballMaterials[i].color;
                SetGradient(enemyBall, ballMaterials[i].color);
            }
        }
    }

    private void SetGradient(GameObject ball, Color color) {
        ParticleSystem ps = ball.transform.GetChild(0).GetComponent<ParticleSystem>();
        var col = ps.colorOverLifetime;
        colorKey[0].color = color;
        gradient.SetKeys(colorKey, alphaKey);
        col.color = gradient;
    }

}
