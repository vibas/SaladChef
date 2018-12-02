using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    int playerScore;
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public int Score
    {
        get
        {
            return playerScore;
        }

        set
        {
            playerScore = value;
        }
    }

    public void IncreaseScore(int score)
    {        
        UpdateScore(score);
    }

    public void DecreaseScore(int score)
    {
        UpdateScore(-score);
    }   

    void UpdateScore(int score)
    {
        playerScore += score;
        player.playerUI.UpdateScore(playerScore.ToString());
    }
}
