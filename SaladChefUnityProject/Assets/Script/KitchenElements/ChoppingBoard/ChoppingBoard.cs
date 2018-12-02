﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoard : InteractibleKitchenElement
{
    ChoppingBoardStateMachine choppingBoardStateMachine;
    Player currentLockedPlayer;
    List<string> currentSalad;
    public SaladMaker saladMaker;

    public GameObject pickSaladButton; // Extra Button for interactible kitchen element

    private void Awake()
    {
        choppingBoardStateMachine = GetComponent<ChoppingBoardStateMachine>();
        currentSalad = new List<string>();
    }

    public override void PlayerReached(Player player)
    {
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

        EnableOrDisableInteractionButton(false);
        EnableOrDisableInteractionButton(pickSaladButton, false);
    }

    /// <summary>
    /// Start the chopping process
    /// </summary>
    /// <param name="player"></param>
    void StartCuttingVegetable(Player player)
    {
        if(!player.AreAllHandsFree())
        {
            string currentVegetableID = player.GetFirstPickedVegetable();
            currentSalad.Add(currentVegetableID);
            saladMaker.AddVegetable(GameManager._instance.vegInventory.GetVegetable(currentVegetableID).choppedItemSprite);
            choppingBoardStateMachine.ChangeState(ChoppingBoardStateMachine.CHOPPING_BOARD_STATE.CHOPPING);
            currentLockedPlayer = player;
            currentLockedPlayer.playerInteraction.StartChoppingVegetable(currentVegetableID);

            if (player.AreAllHandsFree())
            {
                EnableOrDisableInteractionButton(false);
            }
        }                 
    }

    void PickUpSalad(Player player)
    {
        if (CheckIfSaladReadyToServe(player))
        {
            List<string> salad = Utility.GetACopyList(currentSalad);
            player.playerInteraction.CarryPreparedSalad(salad);
            EnableOrDisableInteractionButton(pickSaladButton, false);
            currentSalad.Clear();
            saladMaker.ClearSalad();
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
        currentLockedPlayer.LockOrUnlockPlayerMovement(false);
        currentLockedPlayer = null;
    }

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
