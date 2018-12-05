using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameOverScreen : MonoBehaviour
{    
    public TextMeshProUGUI gameResultText;
    public TextMeshProUGUI gameScoreText;

    public Transform scrollContent;
    public GameObject scoreItemPrefab;
    List<ScoreItem> scoreItemList;

    public void DisplayResult()
    {
        gameResultText.text = GameManager._instance.playerManagerInstance.GetWinningPlayerName();
        gameScoreText.text = GameManager._instance.playerManagerInstance.GetEachPlayerScore();
    }

    public void DisplayTopTenScore(IOrderedEnumerable<KeyValuePair<int, string>> highestScoreDict)
    {
        ClearPreviousScoreItemList();
        scoreItemList = new List<ScoreItem>();

        int counter = 0;
        foreach (KeyValuePair<int, string> item in highestScoreDict)
        {
            counter++;
            if (counter > 10)
            {
                break;
            }
            GameObject scoreItem = Instantiate(scoreItemPrefab) as GameObject;
            scoreItem.transform.SetParent(scrollContent);
            scoreItem.transform.localScale = Vector3.one;

            ScoreItem scoreItemScript = scoreItem.GetComponent<ScoreItem>();
            scoreItemScript.InitScoreItem(item.Value, item.Key);
            scoreItemList.Add(scoreItemScript);            
        }
    }

    void ClearPreviousScoreItemList()
    {
        if(scoreItemList!=null && scoreItemList.Count>0)
        {
            for (int i = scoreItemList.Count-1; i >= 0; i--)
            {
                Destroy(scoreItemList[i].gameObject);
            }
            scoreItemList.Clear();
            scoreItemList = null;
        }        
    }
}
