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

    [SerializeField]
    List<Player> allPlayers;

    public GameConfiguration gameConfig;
    public Inventory vegInventory;
    [HideInInspector]
    public SaladMeuManager saladMeuManager;
    public CustomerSpawnManager customerSpawnManager;
    public PlayerSpawnManager playerSpawnManager;
    public UIManager uiManager;

    public bool isGameOver = false;

    private void Start()
    {
        allPlayers = new List<Player>();

        playerSpawnManager.Init();
        vegInventory = new Inventory(gameConfig.vegetableArray);
        saladMeuManager = GetComponent<SaladMeuManager>();
        saladMeuManager.CreateMenu();

        // For first time, 2 customers are spawned for 2 players.
        customerSpawnManager.SpawnCustomer();
        customerSpawnManager.SpawnCustomer();

        customerSpawnManager.StartTImer();

        for (int i = 0; i < allPlayers.Count; i++)
        {
            uiManager.hudInstance.InitPlayerHUDUI(allPlayers[i]);
            allPlayers[i].playerTimerController.StartTimer();
            allPlayers[i].playerTimerController.onTimerFinished += OnPlayerTimerFinished;
        }
    } 
    
    public void AddPlayerToAllPlayerList(Player player)
    {
        if(!allPlayers.Contains(player))
        {
            allPlayers.Add(player);
        }
    }  
    
    public void OnPlayerTimerFinished()
    {
        if(AreBothPlayerTimerCompleted())
        {
            Debug.LogError("Game Over");
            isGameOver =true;

            for (int i = 0; i < allPlayers.Count; i++)
            {
                allPlayers[i].LockOrUnlockPlayerMovement(true);
            }
        }
    }

    bool AreBothPlayerTimerCompleted()
    {
        bool isBothPlayerTimerCompleted = true;
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if(!allPlayers[i].playerTimerController.isTimerFinished)
            {
                isBothPlayerTimerCompleted = false;
                break;
            }
        }
        return isBothPlayerTimerCompleted;
    }
}
