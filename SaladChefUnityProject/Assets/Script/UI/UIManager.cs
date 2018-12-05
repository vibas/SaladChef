using UnityEngine;

/// <summary>
/// Attached to UIManager GameObject
/// Holds All screen reference
/// All UI operation happens through this class
/// </summary>
public class UIManager : MonoBehaviour
{
    public HUD hudInstance;                 // Player's HUD 
    public GameOverScreen gameOverScreen;   // Game Over screen reference   

    public GameObject gameStartPanel;
    public GameObject pausePanel;

    private void Start()
    {
        gameStartPanel.SetActive(true);

        if (GameManager._instance!=null)
        {
            GameManager._instance.onGamePause += ShowPausePanel;
            GameManager._instance.onGameResume += HidePausePanel;
        }        
    }

    private void OnDisable()
    {
        if (GameManager._instance != null)
        {
            GameManager._instance.onGamePause -= ShowPausePanel;
            GameManager._instance.onGameResume -= HidePausePanel;
        }           
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        GameManager._instance.isGamePaused = false;
        pausePanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.DisplayResult();
    }
}
