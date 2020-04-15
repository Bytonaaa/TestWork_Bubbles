using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenuMaster : MonoBehaviour
{
    public string m_gameScene = "GameScene";
    public Text m_scoreField;
    public GameObject m_menuPanel;

    public void ShowGameEnd(int score)
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
