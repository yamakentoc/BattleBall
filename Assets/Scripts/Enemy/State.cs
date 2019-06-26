using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステート
/// </summary>

public class State<T> {
    //このステートを利用するインスタンス
    protected T owner;

    public State(T owner) {
        this.owner = owner;
    }
    //このステートに遷移する時に一度だけ呼ばれる
    public virtual void Enter() { }
    //このステートである間、毎フレーム呼ばれる(Update)
    public virtual void Execute() { }
    //このステートである間、毎フレーム呼ばれる(fixedUpdate)
    public virtual void FixedExecute() { }
    //このステートから他のステートに遷移する時に一度だけ呼ばれる
    public virtual void Exit() { }
}
