using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Attached to GameManager Game Object
/// Handles saving and reading game data - Highest Score 
/// </summary>
public class SessionSaveManager : MonoBehaviour
{
    const string HIGHEST_SCORE = "HIGHEST_SCORE";
    public Dictionary<int, string> highestScoreDict;

    public void InitHighestScoreDict()
    {
        highestScoreDict = new Dictionary<int, string>();
        if (PlayerPrefs.HasKey(HIGHEST_SCORE))
        {            
            string highestScoreString = PlayerPrefs.GetString(HIGHEST_SCORE);
            string[] scoreData;
            if (highestScoreString.Contains("#"))
            {
                scoreData = highestScoreString.Split('#');
                if(scoreData.Length>0)
                {
                    for (int i = 0; i < scoreData.Length; i++)
                    {
                        if(string.IsNullOrEmpty(scoreData[i]))
                        {
                            continue;
                        }
                        string playerName = scoreData[i].Split(',')[1];
                        int playerScore = int.Parse(scoreData[i].Split(',')[0]);
                        highestScoreDict.Add(playerScore, playerName);
                    }
                }
            }       
        }
    }

    public void SaveHighestScore(string playerName, int score)
    {
        if(highestScoreDict!=null)
        {
            if(score!=0)
            {
                if (highestScoreDict.Count == 0)
                {
                    highestScoreDict.Add(score, playerName);
                }
                else
                {                    
                    if(!highestScoreDict.ContainsKey(score))
                    {
                        highestScoreDict.Add(score, playerName);
                    }
                }
            }

            var highestScoreSorted = from keyValuepair in highestScoreDict
                                     orderby keyValuepair.Key descending
                                     select keyValuepair;

            string scoreItemString = "";            
            foreach (KeyValuePair<int,string> scoreItem in highestScoreSorted)
            {
                scoreItemString += scoreItem.Key.ToString() + "," + scoreItem.Value.ToString() + "#";                
            }

            GameManager._instance.uiManagerInstance.gameOverScreen.DisplayTopTenScore(highestScoreSorted);

            if (!string.IsNullOrEmpty(scoreItemString))
            {                
                PlayerPrefs.SetString(HIGHEST_SCORE, scoreItemString);
            }
        }
    }
}
