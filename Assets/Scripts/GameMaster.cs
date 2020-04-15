using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public event UnityAction<int> ChangeScoreEventHandler;
    public event UnityAction<int> GameEndEventHandler;
    
    public int m_score
    {
        get => _score;
        set => ChangeScoreEventHandler?.Invoke(_score = value);
    }

    [Range(1f, 60f)]
    public float m_playSeconds = 60;
    public float m_secondsLeft { get; private set; }
    
    private float _startTime;
    private int _score;
    
    private void Awake()
    {
        m_score = 0;
        _startTime = Time.time;
        m_secondsLeft = 0f;
    }
    
    private void Update()
    {
        m_secondsLeft = Time.time - _startTime;
        if (m_secondsLeft >= m_playSeconds)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        GameEndEventHandler?.Invoke(m_score);
        enabled = false;
    }
}
