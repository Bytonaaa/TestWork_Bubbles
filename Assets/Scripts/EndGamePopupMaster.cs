using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class EndGamePopupMaster : MonoBehaviour
{
    public string m_gameScene = "GameScene";
    public Text m_scoreField;
    public GameObject m_menuPanel;

    [Inject] 
    private readonly GameMaster _gameMaster = default;
    
    private void OnEnable()
    {
        _gameMaster.GameEndEventHandler += OnGameEnd;
    }

    private void OnDisable()
    {
        _gameMaster.GameEndEventHandler -= OnGameEnd;
    }

    private void OnGameEnd(int score)
    {
        if (m_scoreField != null)
        {
            m_scoreField.text = $"Score: {score}";
        }
        else
        {
            Debug.LogError("Score Field property is null");
        }
        
        if (m_menuPanel != null)
        {
            m_menuPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Menu Panel property is null");
        }
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(m_gameScene);
    }
}
