using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public PlayerInteraction playerInteraction;
    [HideInInspector]
    public PlayerScoreController playerScoreController;
    [HideInInspector]
    public PlayerTimerController playerTimerController;

    public PlayerInputConfig inputConfig;
    public float movementSpeed;

    public int playerID;
    public string playerName;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer[] playerHandItemSprite;     // Make sure this array size should be equal to maxHoldingItemCapacity
   
    private int maxHoldingItemCapacity;
    [SerializeField]
    private bool isAnyHandFree = false;
    public bool isPlayerCarryingSalad;
    bool isPlayerMovementLocked;

    private int totalItemsInPlayerHand;
    [SerializeField]
    private string[] itemsInPlayerHand;
    Queue vegetableQuaue;   

    public List<string> currentSalad;

    public PlayerUI playerUI;
    // Tray in player's hand
    [SerializeField]
    private GameObject tray,saladOnPlate;
    private BoxCollider2D trayCollider;    

    /// <summary>
    /// Initialize Player Property
    /// </summary>
    /// <param name="playerConfig"></param>
    public void InitPlayer(PlayerConfig playerConfig)
    {
        playerID = playerConfig.playerID;
        playerName = playerConfig.playerName;
        name = playerName;

        playerSpriteRenderer.sprite = playerConfig.playerImage;

        playerInteraction = GetComponent<PlayerInteraction>();
        playerScoreController = GetComponent<PlayerScoreController>();
        playerTimerController = GetComponent<PlayerTimerController>();

        inputConfig = playerConfig.inputConfig;

        playerTimerController.TotalTime = GameManager._instance.gameConfig.playerInitialTotalTimer;
        movementSpeed = playerConfig.initialMovementSpeed;
        maxHoldingItemCapacity = GameManager._instance.gameConfig.playerMaxHoldingCapacity;

        itemsInPlayerHand = new string[this.maxHoldingItemCapacity];
        isAnyHandFree = true;
        totalItemsInPlayerHand = 0;        

        vegetableQuaue = new Queue();

        isPlayerMovementLocked = false;
        isPlayerCarryingSalad = false;

        tray.SetActive(false);
        trayCollider = GetComponent<BoxCollider2D>();
        trayCollider.enabled = false;
    }	

    /// <summary>
    /// We check if any hand is or not. If we find free, then we return index.
    /// </summary>
    /// <returns></returns>
    int GetFreeHandIndex()
    {
        int canPick = -1;        
        for (int i = 0; i < itemsInPlayerHand.Length; i++)
        {
            if(string.IsNullOrEmpty(itemsInPlayerHand[i]))
            {
                canPick = i;
                break;
            }
        } 
        return canPick;
    }    
    
    /// <summary>
    /// Check if player's hand is free to pick any item
    /// </summary>
    /// <returns></returns>
    public bool IsAnyHandFree()
    {
        return isAnyHandFree;
    }

    /// <summary>
    /// Check if Player is not carrying anything.
    /// </summary>
    /// <returns></returns>
    public bool AreAllHandsFree()
    {
        bool areAllHandsFree = false;
        if(vegetableQuaue == null || vegetableQuaue.Count==0)
        {
            areAllHandsFree = true;
        }
        return areAllHandsFree;
    }

    /// <summary>
    /// Adds item to player's hand 
    /// </summary>
    /// <param name="itemID"></param>
    public void AddItemToPlayerHand(string itemID)
    {
        int freeHandIndex = GetFreeHandIndex();        
        itemsInPlayerHand[freeHandIndex] = itemID;
        Vegetable veg = GameManager._instance.vegInventory.GetVegetable(itemID);
        playerHandItemSprite[freeHandIndex].sprite = veg.itemSprite;
        totalItemsInPlayerHand++;

        if(totalItemsInPlayerHand== maxHoldingItemCapacity)
        {
            isAnyHandFree = false;
        }

        vegetableQuaue.Enqueue(itemID);
    }

    /// <summary>
    /// Removes Item from Player's Hand
    /// </summary>
    /// <param name="itemID"></param>
    public void RemoveItemFromHand(string itemID)
    {
        for (int i = 0; i < itemsInPlayerHand.Length; i++)
        {
            if (itemsInPlayerHand[i] == itemID)
            {
                itemsInPlayerHand[i] = "";
                playerHandItemSprite[i].sprite = null;
                break;
            }
        }
        totalItemsInPlayerHand--;
        isAnyHandFree = true;
        vegetableQuaue.Dequeue();
    }

    /// <summary>
    /// To get the vegetable, which was picked up by the player first.
    /// </summary>
    /// <returns></returns>
    public string GetFirstPickedVegetable()
    {
        if (vegetableQuaue.Count > 0)
            return vegetableQuaue.Peek().ToString();
        else
            return "";
    }

    /// <summary>
    /// True - Locks player movement
    /// False - Unlocks Player movement
    /// </summary>
    /// <param name="locked"></param>
    public void LockOrUnlockPlayerMovement(bool locked)
    {   
        isPlayerMovementLocked = locked;
    }

    /// <summary>
    /// If player is making salad, he/she can't move. 
    /// </summary>
    /// <returns></returns>
    public bool CanMove()
    {
        return !isPlayerMovementLocked;
    } 

    /// <summary>
    /// Checks if player is currently carrying salad
    /// </summary>
    /// <returns></returns>
    public bool IsCarryingSalad()
    {
        return isPlayerCarryingSalad;
    }

    /// <summary>
    /// Enable Or Disable plate with salad in player's hand
    /// </summary>
    /// <param name="shouldSHow"></param>
    public void ShowOrHideSaladInPlayerHand(bool shouldSHow)
    { 
        if(shouldSHow)
        {
            saladOnPlate.GetComponent<SaladMaker>().CreateSalad(currentSalad);
        }
        else
        {
            saladOnPlate.GetComponent<SaladMaker>().ClearSalad();
        }
        tray.SetActive(shouldSHow);
        trayCollider.enabled = shouldSHow;
    }
}
