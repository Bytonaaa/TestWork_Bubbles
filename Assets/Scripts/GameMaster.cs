using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoSingleton<GameMaster>
{
    public event UnityAction<int> ChangeScoreEventHandler;
    public event UnityAction<int> GameEndEventHandler;
    
    public int m_score
    {
        get => _score;
        set => ChangeScoreEventHandler?.Invoke(_score = value);
    }

    public float m_playSeconds = 60;
    public float m_secondsLeft;
    public bool m_isGameActive;
    
    private float _startTime;
    private int _score;
    
    
    private void Awake()
    {
        m_isGameActive = true;
        m_score = 0;
        _startTime = Time.time;
        m_secondsLeft = 0f;
    }
    
    private void Update()
    {
        m_secondsLeft = Time.time - _startTime;
        if (m_secondsLeft >= m_playSeconds)
        {
            GameEndEventHandler?.Invoke(m_score);
        }

        m_isGameActive = false;
    }
}
