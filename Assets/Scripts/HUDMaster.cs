using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HUDMaster : MonoBehaviour
{
    public Text m_scoreField;
    public Text m_timerField;

    [Inject] private readonly GameMaster _gameMaster = default;
    private void Awake()
    {
        m_timerField.text = m_scoreField.text = string.Empty;
    }

    private void OnEnable()
    {
        _gameMaster.ChangeScoreEventHandler += OnChangeScore;
        _gameMaster.GameEndEventHandler += OnGameEnd;
    }

    private void OnDisable()
    {
        _gameMaster.ChangeScoreEventHandler -= OnChangeScore;
        _gameMaster.GameEndEventHandler -= OnGameEnd;
    }
    
    private void Update()
    {
        var timer = _gameMaster.m_playSeconds - _gameMaster.m_secondsLeft;

        if (m_timerField == null)
        {
            Debug.LogWarning("Timer Field property is null");
            return;
        }
        m_timerField.text = timer.ToString("0.00");
    }

    private void OnGameEnd(int score)
    {
        gameObject.SetActive(false);
    }
    
    private void OnChangeScore(int score)
    {
        if (m_scoreField == null)
        {
            Debug.LogWarning("Score Field property is null");
            return;
        }
        m_scoreField.text = score.ToString();
    }
}
