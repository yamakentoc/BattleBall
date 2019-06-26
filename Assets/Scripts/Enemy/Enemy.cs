using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StatefulObjectBase<Enemy, EnemyState> {


    void Start() {
        Initialize();
    }

    public void Initialize() {

    }

    #region StateBallCollect
    /// <summary>
    /// 落ちてるボールを集める
    /// </summary>
    private class StateBallCollect : State<Enemy> {
        public StateBallCollect(Enemy owner) : base(owner) { }

        public override void Enter() {

        }

        public override void Execute() {

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
