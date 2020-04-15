using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMaster : MonoBehaviour
{
    public string m_gameScene = "GameScene";
    
    public void StartNewGame()
    {
        SceneManager.LoadScene(m_gameScene);
    }
}
