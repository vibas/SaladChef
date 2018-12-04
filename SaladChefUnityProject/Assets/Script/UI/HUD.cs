using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public PlayerUI player1UI;
    public PlayerUI player2UI;

    public void InitPlayerHUDUI(Player player)
    {        
        switch(player.playerID)
        {
            case 1:
                SetInitialValue(player1UI,player);                
                break;
            case 2:
                SetInitialValue(player2UI,player);
                break;
        }        
    }

    void SetInitialValue(PlayerUI playerUI, Player player)
    {
        playerUI.playerTimerToObserver = player.playerTimerController;
        playerUI.UpdatePlayerName(player.playerName);
        playerUI.UpdateScore("0");
        player.playerUI = playerUI;
    }

    private void Update()
    {
        if(player1UI.playerTimerToObserver)
            player1UI.UpdateTime(player1UI.playerTimerToObserver.GetCurrentTimeString());
        if (player2UI.playerTimerToObserver)
            player2UI.UpdateTime(player2UI.playerTimerToObserver.GetCurrentTimeString());
    }   
}

[System.Serializable]
public class PlayerUI
{
    [HideInInspector]
    public PlayerTimerController playerTimerToObserver;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI playerTimerText;

    public void UpdatePlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }

    public void UpdateScore(string score)
    {
        playerScoreText.text = score;
    }

    public void UpdateTime(string time)
    {
        playerTimerText.text = time;
    }
}
