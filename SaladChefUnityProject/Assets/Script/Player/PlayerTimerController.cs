using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to Player Game Object
/// Handles each player's timer counting (increase timer if powerup is activated)
/// </summary>
public class PlayerTimerController : MonoBehaviour
{
    int totalTimer;    
    public float currentTimer;
    bool shouldRunTimer = false;
    public bool isTimerFinished = false;

    public delegate void OnTimerFinished();
    public OnTimerFinished onTimerFinished;

    public int TotalTime
    {
        set
        {
            totalTimer = value;
        }

        get
        {
            return totalTimer;
        }
    }

    public void IncreaseTimer(int extraTimer)
    {
        // As we are incresing current timer in update, we are deducting here to increase remaining timer
        currentTimer -= (float)extraTimer;        
    }

    int GetCurrentTime()
    {
        return Mathf.RoundToInt(currentTimer);
    }

    int GetRemainingTimer()
    {
        return TotalTime - GetCurrentTime();
    }

    /// <summary>
    /// Get Remaining Time in time format
    /// </summary>
    /// <returns></returns>
    public string GetCurrentTimeString()
    {
        string minutes = Mathf.Floor(GetRemainingTimer() / 60).ToString("00");
        string seconds = (GetRemainingTimer() % 60).ToString("00");

        return string.Format("{0}:{1}", minutes, seconds);
    }

    public void StartTimer()
    {
        shouldRunTimer = true;
    }

    public void StopTimer()
    {
        shouldRunTimer = false;
    }

    private void Update()
    {
        if(shouldRunTimer)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer>totalTimer)
            {
                Player currentPlayer = GetComponent<Player>();
                currentPlayer.LockOrUnlockPlayerMovement(true);                
                isTimerFinished = true;
                shouldRunTimer = false;
                onTimerFinished();
            }
        }
    }
}
