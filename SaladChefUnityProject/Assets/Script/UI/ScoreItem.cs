using UnityEngine;
using TMPro;

/// <summary>
/// Each player score item containing name and score
/// </summary>
public class ScoreItem : MonoBehaviour
{
    public TextMeshProUGUI playerNameText, playerScoreText;

	public void InitScoreItem(string playerName, int playerScore)
    {
        playerNameText.text = playerName;
        playerScoreText.text = playerScore.ToString();
    }
}
