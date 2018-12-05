using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to each chopping board
/// </summary>
public class ChoppingBoard : InteractibleKitchenElement
{
    ChoppingBoardStateMachine choppingBoardStateMachine;

    Player currentLockedPlayer;         // Player who is using this chop board
    List<string> currentSalad;          // Holds ingredient IDs
    public SaladMaker saladMaker;
    bool isOccupied;

    public GameObject pickSaladButton;  // Extra Button for interactible kitchen element

    private void Awake()
    {
        choppingBoardStateMachine = GetComponent<ChoppingBoardStateMachine>();
        currentSalad = new List<string>();
        isOccupied = false; 
    }

    private void Start()
    {        
        GameManager._instance.onResetGame += ClearChoppingBoard;
        GameManager._instance.onGamePause += PauseChopping;
        GameManager._instance.onGameResume += ResumeChopping;

        choppingBoardStateMachine.InitStateMachine();
    }    

    /// <summary>
    /// Clear salad list and remove sprite from chopping board
    /// </summary>
    public void ClearChoppingBoard()
    {
        GameManager._instance.onResetGame -= ClearChoppingBoard;
        GameManager._instance.onGamePause -= PauseChopping;
        GameManager._instance.onGameResume -= ResumeChopping;

        isOccupied = false;
        saladMaker.ClearSalad();
        currentSalad.Clear();
        currentLockedPlayer = null;

        choppingBoardStateMachine.ResetTimer();

        ResetChoppingBoard();
    }

    /// <summary>
    /// Called when player restarts the game
    /// </summary>
    void ResetChoppingBoard()
    {
        GameManager._instance.onResetGame += ClearChoppingBoard;
        GameManager._instance.onGamePause += PauseChopping;
        GameManager._instance.onGameResume += ResumeChopping;

        choppingBoardStateMachine.InitStateMachine();
    }

    void PauseChopping()
    {
        choppingBoardStateMachine.PauseTimer();
    }

    void ResumeChopping()
    {
        choppingBoardStateMachine.ResumeTimer();
    }

    public override void PlayerReached(Player player)
    {
        // Dont allow another player interrupt current player
        if (isOccupied)
            return;

        base.PlayerReached(player);
        player.playerInteraction.onPutKeyPressed += StartCuttingVegetable;
        player.playerInteraction.onPickKeyPressed += PickUpSalad;

        // If Player is holding any vegetable
        if (!player.AreAllHandsFree()) 
            EnableOrDisableInteractionButton(true,player.inputConfig.putKey.ToString());

        // If Player is not holding any vegetable and chopped veg is ready on board
        if(CheckIfSaladReadyToServe(player))
        {            
            EnableOrDisableInteractionButton(pickSaladButton, true, player.inputConfig.pickKey.ToString());
        }
    }

    public override void PlayerLeft(Player player)
    { 
        base.PlayerLeft(player);
        player.playerInteraction.onPutKeyPressed -= StartCuttingVegetable;
        player.playerInteraction.onPickKeyPressed -= PickUpSalad;

        isOccupied = false;

        EnableOrDisableInteractionButton(false);
        EnableOrDisableInteractionButton(pickSaladButton, false);
    }

    /// <summary>
    /// Start the chopping process
    /// </summary>
    /// <param name="player"></param>
    public void StartCuttingVegetable(Player player)
    {      
        if(choppingBoardStateMachine.choppingBoardCurrentState == ChoppingBoardStateMachine.CHOPPING_BOARD_STATE.CHOPPING)
        {
            return;
        }

        if (!player.AreAllHandsFree())
        {
            string currentVegetableID = player.GetFirstPickedVegetable();
            if(currentVegetableID=="")
            {
                Debug.LogError("Chopping board hand empty");
                return;
            }
            currentSalad.Add(currentVegetableID);
            saladMaker.AddVegetable(GameManager._instance.vegInventory.GetVegetable(currentVegetableID).choppedItemSprite);
            choppingBoardStateMachine.ChangeState(ChoppingBoardStateMachine.CHOPPING_BOARD_STATE.CHOPPING);
            isOccupied = true;
            currentLockedPlayer = player;
            currentLockedPlayer.playerInteraction.StartChoppingVegetable(currentVegetableID);
            EnableOrDisableInteractionButton(false);            
        }                 
    }

    void PickUpSalad(Player player)
    {
        if (choppingBoardStateMachine.choppingBoardCurrentState == ChoppingBoardStateMachine.CHOPPING_BOARD_STATE.CHOPPING)
        {
            return;
        }

        if (CheckIfSaladReadyToServe(player))
        {
            List<string> salad = Utility.GetACopyList(currentSalad);
            player.playerInteraction.CarryPreparedSalad(salad);
            EnableOrDisableInteractionButton(pickSaladButton, false);
            currentSalad.Clear();
            saladMaker.ClearSalad();
            isOccupied = false;
        }
        else
        {
            Debug.LogError("Salad is not ready");
        }
    }    

    /// <summary>
    /// Get the player who is busy in chopping vegetable
    /// </summary>
    /// <returns></returns>
    public Player GetCurrentLockedPlayer()
    {
        return currentLockedPlayer;
    }

    /// <summary>
    /// Once Player finished chopping, he/she can move
    /// </summary>
    public void FreeUpPlayer()
    {
        if (currentLockedPlayer.playerTimerController.isTimerFinished)
            return;
        currentLockedPlayer.LockOrUnlockPlayerMovement(false);
        currentLockedPlayer = null;              
    }

    /// <summary>
    /// If there is salad on chopping board and player's both hands are free to carry it
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CheckIfSaladReadyToServe(Player player)
    {
        bool saladReadyToServe = false;
        if (player.AreAllHandsFree())
        {
            if (currentSalad.Count > 0)
            {
                saladReadyToServe = true;
            }
        }
        return saladReadyToServe;
    }
}
