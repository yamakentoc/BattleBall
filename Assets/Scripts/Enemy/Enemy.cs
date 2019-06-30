using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : StatefulObjectBase<Enemy, EnemyState> {

    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] ParticleSystem particle;
    [SerializeField] NamePlate namePlate;
    private Vector3 previousScale;
    private Vector2 startPos, nowPos, differenceDisVector2;
    private float speed = 20, radian, speedUpTime;
    private bool foundBall, outSideSafeArea;
    private GameObject targetBall;

    void Start() {
        Initialize();
        namePlate.PlayerName = "Enemy";
    }

    public void Initialize() {
        stateList.Add(new StateBallCollect(this));
        stateList.Add(new StateTracking(this));
        stateList.Add(new StateAttack(this));
        stateList.Add(new StateEscape(this));
        stateMachine = new StateMachine<Enemy>();
        ChangeState(EnemyState.BallCollect);
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("NeutralBall")) {
            Debug.Log("ボールゲット");
            foundBall = false;
            targetBall = null;
            transform.localScale = new Vector3(transform.localScale.x + 0.05f, transform.localScale.y + 0.05f, transform.localScale.z + 0.05f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 2, transform.localPosition.z);
            rigidbody.mass = transform.localScale.x / 3.0f;
            Vector3 diffScale = transform.localScale - previousScale;
            namePlate.ChangePosition(diffScale);
            particle.transform.localScale += diffScale / 3.0f;
            particle.startLifetime += diffScale.x / 4.0f;
            previousScale = transform.localScale;
            Destroy(other.gameObject);

        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("SafeArea")) {
            Debug.Log("外に出た!");
            outSideSafeArea = true;
            foundBall = false;
            targetBall = null;
            radian = Mathf.Atan2(-transform.position.x, -transform.position.z) * Mathf.Rad2Deg;
        }
    }

    #region StateBallCollect
    /// <summary>
    /// 落ちてるボールを集める
    /// </summary>
    private class StateBallCollect : State<Enemy> {
        public StateBallCollect(Enemy owner) : base(owner) { }
        List<Collider> colliders = new List<Collider>();
        private float randomTime, outSideSafeArea;
       
        public override void Enter() { }

        public override void Execute() {
            if (owner.outSideSafeArea && outSideSafeArea < 2.0f) {
                outSideSafeArea += Time.deltaTime;
                return;
            }
            owner.outSideSafeArea = false;
            outSideSafeArea = 0f;
            if (!owner.foundBall) {
                SearchAroundBall();
                if (colliders.Count == 0) {
                    randomTime -= Time.deltaTime;
                    if (randomTime <= 0.0f) {
                        randomTime = 2f;
                        owner.radian = Random.Range(-179f, 180f);
                        Debug.Log("ランダム");
                    }
                } else {
                    Debug.Log("ボール発見");
                    float minDistance = Vector3.Distance(owner.transform.position, colliders[0].gameObject.transform.position);
                    foreach (Collider collider in colliders) {
                        float distance = Vector3.Distance(owner.transform.position, collider.gameObject.transform.position);
                        if (distance <= minDistance) {
                            minDistance = distance;
                            owner.targetBall = collider.gameObject;
                            owner.targetBall.GetComponent<Renderer>().material.color = Color.red;
                            owner.foundBall = true;
                        }
                    }
                }
            }
            if (owner.targetBall != null) {
                owner.differenceDisVector2 = new Vector2(owner.targetBall.transform.position.x - owner.transform.position.x, owner.targetBall.transform.position.z - owner.transform.position.z);
                owner.radian = Mathf.Atan2(owner.differenceDisVector2.x, owner.differenceDisVector2.y) * Mathf.Rad2Deg;
            }
        }

        public override void FixedExecute() {
            owner.rigidbody.AddForce(5 * (owner.transform.forward * owner.speed - owner.rigidbody.velocity));
            owner.rigidbody.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.Euler(0, owner.radian, 0), 1);
        }

        public override void Exit() { }

        private void SearchAroundBall() {
            colliders = Physics.OverlapSphere(owner.transform.position, 3).ToList();
            colliders = colliders.Where(collider => collider.gameObject.tag.Equals("NeutralBall")).ToList();
        }

    }
    #endregion

    #region StateTracking
    /// <summary>
    /// 他のプレイヤーを追従する
    /// </summary>
    private class StateTracking : State<Enemy> {
        public StateTracking(Enemy owner) : base(owner) { }

        public override void Enter() {

        }

        public override void Execute() {

        }

        public override void Exit() {

        }

    }
    #endregion

    #region StateAttack
    /// <summary>
    /// 他のプレイヤーを攻撃する
    /// </summary>
    private class StateAttack : State<Enemy>
    {
        public StateAttack(Enemy owner) : base(owner) { }

        public override void Enter() {

        }

        public override void Execute() {

        }

        public override void Exit() {

        }

    }
    #endregion

    #region StateEscape
    /// <summary>
    /// 他のプレイヤーから逃げる
    /// </summary>
    private class StateEscape : State<Enemy> {
        public StateEscape(Enemy owner) : base(owner) { }

        public override void Enter() {

        }

        public override void Execute() {

        }

        public override void Exit() {

        }

    }
    #endregion
}
