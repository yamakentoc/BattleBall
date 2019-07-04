﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{

    private GUIStyle m_guiStyle;
    private GUIStyleState m_styleState;
    // 変数
    int frameCount;
    float prevTime;
    float fps;

    // 初期化処理
    void Start()
    {
        // 変数の初期化
        frameCount = 0;
        prevTime = 0.0f;

        m_guiStyle = new GUIStyle();
        m_guiStyle.fontSize = 50;

        m_styleState = new GUIStyleState();
        m_styleState.textColor = Color.white;   // 文字色の変更.
        m_guiStyle.normal = m_styleState;
    }

    // 更新処理
    void Update()
    {
        frameCount++;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            fps = frameCount / time;
            Debug.Log(fps);

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }

    // 表示処理
    private void OnGUI()
    {
        //GUILayout.Label(fps.ToString());
        GUI.Label(new Rect(20, 40, 100, 40), fps.ToString(), m_guiStyle);
    }
}