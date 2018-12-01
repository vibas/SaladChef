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

    public GameConfiguration gameConfig;
    public Inventory vegInventory;
    [HideInInspector]
    public SaladMeuManager saladMeuManager;
    public CustomerSpawnManager customerSpawnManager;

    private void Start()
    {
        vegInventory = new Inventory(gameConfig.vegetableArray);
        saladMeuManager = GetComponent<SaladMeuManager>();
        saladMeuManager.CreateMenu();
        customerSpawnManager.InitiateAllCustomer();
    }  
}
