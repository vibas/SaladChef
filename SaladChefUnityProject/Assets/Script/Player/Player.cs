using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInteraction playerInteraction;

    public PlayerInputConfig inputConfig;
    public float movementSpeed;

    public int playerID;
    public string playerName;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer[] playerHandItemSprite;     // Make sure this array size should be equal to maxHoldingItemCapacity

    private int maxHoldingItemCapacity;
    [SerializeField]
    private bool isAnyHandFree = false;    
    private int totalItemsInPlayerHand;
    [SerializeField]
    private string[] itemsInPlayerHand;
    Queue items;
    bool isPlayerMakingSalad;

    public List<string> currentSalad;
    bool isPlayerCarryingSalad;

    /// <summary>
    /// Initialize Player Property
    /// </summary>
    /// <param name="playerConfig"></param>
    public void InitPlayer(PlayerConfig playerConfig)
    {
        playerID = playerConfig.playerID;
        playerName = playerConfig.playerName;
        this.name = playerName;
        playerSpriteRenderer.sprite = playerConfig.playerImage;

        inputConfig = playerConfig.inputConfig;
        movementSpeed = playerConfig.initialMovementSpeed;

        this.maxHoldingItemCapacity = GameManager._instance.gameConfig.playerMaxHoldingCapacity;

        itemsInPlayerHand = new string[this.maxHoldingItemCapacity];
        this.isAnyHandFree = true;
        this.totalItemsInPlayerHand = 0;

        playerInteraction = GetComponent<PlayerInteraction>();
        items = new Queue();
        isPlayerMakingSalad = false;
        isPlayerCarryingSalad = false;
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
        if(items == null || items.Count==0)
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

        items.Enqueue(itemID);
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
        items.Dequeue();
    }

    /// <summary>
    /// To get the vegetable, which was picked up by the player first.
    /// </summary>
    /// <returns></returns>
    public string GetFirstPickedVegetable()
    {
        return items.Peek().ToString();
    }

    /// <summary>
    /// True - Locks player movement
    /// False - Unlocks Player movement
    /// </summary>
    /// <param name="locked"></param>
    public void LockOrUnlockPlayerMovement(bool locked)
    {
        isPlayerMakingSalad = locked;
    }

    /// <summary>
    /// If player is making salad, he/she can't move. 
    /// </summary>
    /// <returns></returns>
    public bool CanMove()
    {
        return !isPlayerMakingSalad;
    }

    public void CarryPreparedSalad(List<string> preparedSalad)
    {
        currentSalad = preparedSalad;
        isPlayerCarryingSalad = true;        
    }

    public void DeliverPreparedSalad()
    {
        currentSalad.Clear();
        isPlayerCarryingSalad = false;
    }

    public bool IsCarryingSalad()
    {
        return isPlayerCarryingSalad;
    }
}
