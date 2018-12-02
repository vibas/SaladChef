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
    } 
    
    public void AddPlayerToAllPlayerList(Player player)
    {
        if(!allPlayers.Contains(player))
        {
            allPlayers.Add(player);
        }
    }    
}
