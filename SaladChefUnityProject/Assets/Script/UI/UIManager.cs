using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public HUD hudInstance;
    public GameObject gameoverPanel;

    public void ShowGameOverPanel()
    {
        gameoverPanel.SetActive(true);
    }
}
