using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の行動状態をenumで管理
/// </summary>

public enum EnemyState {
    BallCollect,
    Tracking,
    Attack,
    Escape
}