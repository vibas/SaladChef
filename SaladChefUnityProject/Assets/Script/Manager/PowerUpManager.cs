using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Attached to GameManager Game Object
/// Handles creation of powerup
/// </summary>
public class PowerUpManager : MonoBehaviour
{ 
    [SerializeField]
    private PowerUpConfig[] allPowerUpConfigArray;
    [SerializeField]
    private GameObject powerUpPrefab;
    [SerializeField]
    private SpriteRenderer powerUpSpawnArea;

    public List<PowerUp> allPowerUps;
    
    private void Start()
    {
        allPowerUps = new List<PowerUp>();        
    }   

    public void ResetAllPowerUps()
    {
        if(allPowerUps !=null && allPowerUps.Count>0)
        {
            for (int i = allPowerUps.Count - 1; i >= 0; i--)
            {
                Destroy(allPowerUps[i].gameObject);
            }            
            allPowerUps.Clear();
        }        
    }

    public void SpawnRandomPowerUpForPlayer(int playerID)
    {
        int randomIndex = Random.Range(0, allPowerUpConfigArray.Length);
        GameObject powerUpObject = Instantiate(powerUpPrefab) as GameObject;        
        powerUpObject.transform.position = GetRandomSpawnPosition();        
        PowerUp powerUp = powerUpObject.GetComponent<PowerUp>();
        powerUp.AssignPowerUpForPlayer(playerID);
        powerUp.InitializePowerUpData(allPowerUpConfigArray[randomIndex]);
        allPowerUps.Add(powerUp);
    }

    Vector3 GetRandomSpawnPosition()
    {
        Bounds area = powerUpSpawnArea.bounds;
        float randomX = Random.Range(area.min.x,area.max.x);
        float randomY = Random.Range(area.min.y, area.max.y);

        return new Vector3(randomX, randomY,0);
    }    
}

public enum POWERUP_TYPE
{
    TIME_BOOSTER,
    SPEED_BOOSTER,
    SCORE_BOOSTER
}
