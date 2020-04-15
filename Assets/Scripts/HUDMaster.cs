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
        m_timerField.text = timer.ToString("#.##");
    }

    private void OnGameEnd(int score)
    {
        gameObject.SetActive(false);
    }
    
    private void OnChangeScore(int score)
    {
        m_scoreField.text = score.ToString();
    }
}
