using UnityEngine;

/// <summary>
/// Attached to GameManager GameObject. 
/// Singleton Class
/// Holds all the manager's reference
/// Holds Game Configuration (customizable)
/// Game states are also managed here (Resume, Pause, Restart, Start etc..)
/// </summary>
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
    [HideInInspector]
    public PowerUpManager powerUpManagerInstance;
    public UIManager uiManagerInstance;
    [HideInInspector]
    public SessionSaveManager sessionSaveManager;

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
        sessionSaveManager = GetComponent<SessionSaveManager>();
        saladMeuManagerInstance = GetComponent<SaladMeuManager>();
        saladMeuManagerInstance.CreateMenu();       
    } 

    public void StartGame()
    {
        sessionSaveManager.InitHighestScoreDict();

        playerManagerInstance.InitPlayers();
        playerManagerInstance.StartPlayerActivity();

        // For first time, 2 customers are spawned for 2 players.
        for (int i = 0; i < 2; i++)
        {
            customerManagerInstance.SpawnCustomer();
        } 
        
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

    /// <summary>
    /// When ever any of the player's timer is completed, we check if both the player's timers are completed or not
    /// </summary>
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
