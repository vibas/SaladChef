using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if(highestScoreDict.Count==0)
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

            string scoreItemString = "";
            foreach (KeyValuePair<int,string> scoreItem in highestScoreDict)
            {
                scoreItemString += scoreItem.Key.ToString() + "," + scoreItem.Value.ToString() + "#";                
            }

            if (!string.IsNullOrEmpty(scoreItemString))
            {
                Debug.LogError("Highest Score = " + scoreItemString);
                PlayerPrefs.SetString(HIGHEST_SCORE, scoreItemString);
            }
        }
    }

}
