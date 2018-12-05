using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public HUD hudInstance;

    public GameObject gameoverPanel;
    public TextMeshProUGUI gameResultText;
    public TextMeshProUGUI gameScoreText;

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
        gameoverPanel.SetActive(true);
        gameResultText.text = GameManager._instance.playerManagerInstance.GetWinningPlayerName();
        gameScoreText.text = GameManager._instance.playerManagerInstance.GetEachPlayerScore();
    }
}
