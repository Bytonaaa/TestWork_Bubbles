using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMaster : MonoBehaviour
{
    public Text m_scoreField;
    public Text m_timerField;
    
    private void OnEnable()
    {
        GameMaster.instance.ChangeScoreEventHandler += OnChangeScore;
    }

    private void OnDisable()
    {
        GameMaster.instance.ChangeScoreEventHandler -= OnChangeScore;
    }

    private void OnChangeScore(int score)
    {
        m_scoreField.text = score.ToString();
    }

    private void Update()
    {
        m_timerField.text = GameMaster.instance.m_secondsLeft.ToString();
    }
}
