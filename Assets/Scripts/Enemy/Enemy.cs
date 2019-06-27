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
    private bool foundBall;
    private GameObject targetBall;

    void Start() {
        Initialize();
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
            //if (other.gameObject == targetBall) {
                //Debug.Log("今です！！");
                foundBall = false;
            //}
        }
    }

    //public void RelayOnTriggerEnter(GameObject ball) {
    //    if (!foundBall) {
    //        Debug.Log("ボールを検出");
    //        foundBall = true;
    //        targetBall = ball;
    //    }
    //}

    #region StateBallCollect
    /// <summary>
    /// 落ちてるボールを集める
    /// </summary>
    private class StateBallCollect : State<Enemy> {
        public StateBallCollect(Enemy owner) : base(owner) { }

        public override void Enter() {

        }

        public override void Execute() {
            if (!owner.foundBall) {
                Collider[] colliders = Physics.OverlapSphere(owner.transform.position, 3);
                List<Collider> colliderList = new List<Collider>(colliders);
                colliderList = colliderList.Where(collider => collider.gameObject.tag.Equals("NeutralBall")).ToList();

                if (colliders.Length == 0) {
                    //ランダムに移動する処理
                    owner.radian = Random.Range(-180f, 180f);
                    Debug.Log("ランダム値: " + owner.radian);
                } else {  
                    float maxDistance = 0.0f;
                    foreach (Collider collider in colliders) {
                        if (!collider.gameObject.tag.Equals("NeutralBall")) { return; }
                            float distance = Vector3.Distance(owner.transform.position, collider.gameObject.transform.position);
                        if (distance > maxDistance) {
                            maxDistance = distance;
                            owner.targetBall = collider.gameObject;
                            owner.foundBall = true;
                        }
                    }
                }
            }

            owner.differenceDisVector2 = new Vector2(-owner.targetBall.transform.position.x, -owner.targetBall.transform.position.z);
            owner.radian = Mathf.Atan2(owner.differenceDisVector2.x, owner.differenceDisVector2.y) * Mathf.Rad2Deg;
            Debug.Log("radian: " + owner.radian);
            
        }

        public override void FixedExecute() {
            owner.rigidbody.AddForce(5 * (owner.transform.forward * owner.speed - owner.rigidbody.velocity));
            owner.rigidbody.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.Euler(0, owner.radian, 0), 1);
        }

        public override void Exit() {

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
