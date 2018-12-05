using UnityEngine;
using System.Collections.Generic;

public class PlayerSpawnManager : MonoBehaviour
{
    [Tooltip("Present inside Kitchen")]
    public Transform[] playerSpawnPointArray;

    public GameObject playerPrefab;

    public List<Player> allPlayers;

    public PlayerConfig player1Config, player2Config;  

	// Use this for initialization
	public void InitPlayers ()
    {
        allPlayers = new List<Player>();

        GameObject player1 = Instantiate(playerPrefab, playerSpawnPointArray[0].position, Quaternion.identity);
        player1.transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0);
        player1.GetComponent<Player>().InitPlayer(player1Config);
        player1.transform.SetParent(transform);        

        GameObject player2 = Instantiate(playerPrefab, playerSpawnPointArray[1].position, Quaternion.identity);
        player2.transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0);
        player2.GetComponent<Player>().InitPlayer(player2Config);
        player2.transform.SetParent(transform);

        AddPlayerToAllPlayerList(player1.GetComponent<Player>());
        AddPlayerToAllPlayerList(player2.GetComponent<Player>());
    }

    void AddPlayerToAllPlayerList(Player player)
    {
        if (!allPlayers.Contains(player))
        {
            allPlayers.Add(player);
        }
    }

    public void ResetPlayer()
    {
        if(allPlayers!=null)
        {
            for (int i = allPlayers.Count - 1; i >= 0; i--)
            {
                Destroy(allPlayers[i].gameObject);
            }
            allPlayers.Clear();
            allPlayers = null;
        }        
    }

    public void StartPlayerActivity()
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            GameManager._instance.uiManagerInstance.hudInstance.InitPlayerHUDUI(allPlayers[i]);
            allPlayers[i].playerTimerController.StartTimer();
            allPlayers[i].playerTimerController.onTimerFinished += GameManager._instance.OnPlayerTimerFinished;
        }
    }

    public void PausePlayerActivity()
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {            
            allPlayers[i].playerTimerController.StopTimer();
            allPlayers[i].LockOrUnlockPlayerMovement(true);
        }        
    }

    public void ResumePlayerActivity()
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            allPlayers[i].playerTimerController.StartTimer();
            allPlayers[i].LockOrUnlockPlayerMovement(false);
        }
    }

    public bool AreBothPlayerTimerCompleted()
    {
        bool isBothPlayerTimerCompleted = true;
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (!allPlayers[i].playerTimerController.isTimerFinished)
            {
                isBothPlayerTimerCompleted = false;
                break;
            }
        }
        return isBothPlayerTimerCompleted;
    }

    public string GetWinningPlayerName()
    {
        string winningPlayer = "";
        int winnerScore = 0;
        string winnerName = "";
        if(allPlayers[0].playerScoreController.Score>allPlayers[1].playerScoreController.Score)
        {
            winningPlayer = allPlayers[0].name + " Wins";
            winnerScore = allPlayers[0].playerScoreController.Score;
            winnerName = allPlayers[0].name;
        }
        else if(allPlayers[0].playerScoreController.Score == allPlayers[1].playerScoreController.Score)
        {
            winningPlayer = "Its a tie.";
        }
        else
        {
            winningPlayer = allPlayers[1].name + " Wins";
            winnerScore = allPlayers[1].playerScoreController.Score;
            winnerName = allPlayers[1].name;
        }
        if(winnerScore>0)
        {
            GameManager._instance.sessionSaveManager.SaveHighestScore(winnerName, winnerScore);
        }
        return winningPlayer;
    }
    
    public string GetEachPlayerScore()
    {
        string score = "";
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (i == 1)
                score += "\t";
            score += allPlayers[i].playerName + " " + allPlayers[i].playerScoreController.Score;
        }
        return score;
    }
}
