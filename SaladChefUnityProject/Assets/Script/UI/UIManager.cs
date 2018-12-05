using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HUD hudInstance;
    public GameOverScreen gameOverScreen;    

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
