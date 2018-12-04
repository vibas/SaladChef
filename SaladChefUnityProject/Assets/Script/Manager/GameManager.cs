using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;    

    private void Awake()
    {
        if(_instance == null)
            _instance = this.GetComponent<GameManager>();
    }

    public delegate void OnResetGame();
    public OnResetGame onResetGame;
    public delegate void OnGamePause();
    public OnGamePause onGamePause;
    public delegate void OnGameResume();
    public OnGameResume onGameResume;

    public GameConfiguration gameConfig;
    public Inventory vegInventory;
    
    public CustomerSpawnManager customerManagerInstance;
    public PlayerSpawnManager playerManagerInstance;
    public PowerUpManager powerUpManagerInstance;
    public UIManager uiManagerInstance;

    [HideInInspector]
    public PlayerRewardSystem playerRewardSystemInstance;
    [HideInInspector]
    public SaladMeuManager saladMeuManagerInstance;

    public bool isGameOver = false;
    public bool isGamePaused = false;
    public bool isGameStarted = false;

    private void Start()
    { 
        vegInventory = new Inventory(gameConfig.vegetableArray);
        playerRewardSystemInstance = GetComponent<PlayerRewardSystem>();
        powerUpManagerInstance = GetComponent<PowerUpManager>();
        saladMeuManagerInstance = GetComponent<SaladMeuManager>();
        saladMeuManagerInstance.CreateMenu();       
    } 

    public void StartGame()
    {
        playerManagerInstance.InitPlayers();
        playerManagerInstance.StartPlayerActivity();

        // For first time, 2 customers are spawned for 2 players.
        customerManagerInstance.SpawnCustomer();
        customerManagerInstance.SpawnCustomer();
        customerManagerInstance.StartWaveTimer();
        customerManagerInstance.StartCustomerTimer();
        isGameStarted = true;
    }   

    void PauseGame()
    {
        isGamePaused = true;
        playerManagerInstance.PausePlayerActivity();

        customerManagerInstance.StopWaveTimer();
        customerManagerInstance.StopCustomerTimer();

        onGamePause();
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        playerManagerInstance.ResumePlayerActivity();

        customerManagerInstance.StartWaveTimer();
        customerManagerInstance.StartCustomerTimer();

        onGameResume();
    }

    void GameOver()
    {
        isGameOver = true;
        isGameStarted = false;
        PauseGame();
        uiManagerInstance.HidePausePanel();
        uiManagerInstance.ShowGameOverPanel();
    }

    public void RestartGame()
    {
        playerManagerInstance.ResetPlayer();
        customerManagerInstance.ResetWaveTimer();
        customerManagerInstance.ResetAllCustomer();
        powerUpManagerInstance.ResetAllPowerUps();
        // For clearing extra plate and chopping board 
        onResetGame();

        isGameOver = false;
        isGamePaused = false;
    }

    public void OnPlayerTimerFinished()
    {
        if (playerManagerInstance.AreBothPlayerTimerCompleted())
        {                       
            GameOver();           
        }
    }

    private void Update()
    {       
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isGamePaused && isGameStarted)
            {                
                PauseGame();
            }            
        }        
    }
}
