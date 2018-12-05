using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreItem : MonoBehaviour
{
    public TextMeshProUGUI playerNameText, playerScoreText;

	public void InitScoreItem(string playerName, int playerScore)
    {
        playerNameText.text = playerName;
        playerScoreText.text = playerScore.ToString();
    }
}
