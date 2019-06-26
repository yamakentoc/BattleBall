using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBallCollider : MonoBehaviour {

    [SerializeField] Enemy enemy;

    void Start() {
        
    }
    
    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("NeutralBall")) {
            enemy.RelayOnTriggerEnter(other);
        }
    }

}
