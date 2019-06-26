using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StatefulObjectBase<Enemy, EnemyState> {

    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] ParticleSystem particle;
    [SerializeField] NamePlate namePlate;
    private Vector3 previousScale;
    private float speed = 20, radian, speedUpTime;

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
        }
    }

    public void RelayOnTriggerEnter(Collider hoge) {
        Debug.Log("ボールを検出");
    }

    #region StateBallCollect
    /// <summary>
    /// 落ちてるボールを集める
    /// </summary>
    private class StateBallCollect : State<Enemy> {
        public StateBallCollect(Enemy owner) : base(owner) { }

        private bool foundBall;

        public override void Enter() {

        }

        public override void Execute() {
            Debug.Log("速さ: " + owner.rigidbody.velocity.magnitude);
            //if (!foundBall) {
                
            //}
        }

        public override void FixedExecute() {
            //owner.rigidbody.AddForce(5 * (owner.transform.forward * owner.speed - owner.rigidbody.velocity));
            //owner.rigidbody.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.Euler(0, owner.radian, 0), 1);
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
