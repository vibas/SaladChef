using UnityEngine;
public class PlayerRewardSystem : MonoBehaviour
{
    /// <summary>
    /// When customer gets satisfied, Player gets some score
    /// </summary>
    /// <param name="player"></param>
    /// <param name="scoreToAdd"></param>
	public void RewardPlayerWithScore(Player player, int scoreToAdd)
    {
        player.playerScoreController.IncreaseScore(scoreToAdd);
    }

    /// <summary>
    /// When Customer gets impressed, Player gets a powerup
    /// </summary>
    /// <param name="player"></param>
    public void RewardPlayerWithPowerUp(Player player)
    {

    }

    /// <summary>
    /// When customer gets angry/dissatisfied, Player will loose some score
    /// </summary>
    /// <param name="player"></param>
    /// <param name="scoreToDeduct"></param>
    public void PunishPlayerWithScore(Player player, int scoreToDeduct)
    {
        player.playerScoreController.DecreaseScore(scoreToDeduct);
    }

    /// <summary>
    /// When customer gets dissatisfied (Item not delivered in time), both the Players will loose some score
    /// </summary>
    /// <param name="scoreToDeduct"></param>
    public void PunishBothPlayerWithScore(int scoreToDeduct)
    {
        for (int i = 0; i < GameManager._instance.playerManagerInstance.allPlayers.Count; i++)
        {
            PunishPlayerWithScore(GameManager._instance.playerManagerInstance.allPlayers[i], scoreToDeduct);
        }
    }
}
