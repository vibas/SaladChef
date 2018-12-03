using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraPlate : InteractibleKitchenElement
{
    public bool isEmpty;
    public Image vegetableOnPlate;
    public string vegetableID;

    private void Start()
    {
        GameManager._instance.onResetGame += ResetExtraPlate;
    }

    void ResetExtraPlate()
    {
        isEmpty = true;
        vegetableOnPlate.sprite = null;
        vegetableOnPlate.enabled = false;
        vegetableID = "";        
    }

    public override void PlayerReached(Player player)
    {
        player.playerInteraction.onPutKeyPressed += PlayerPlacedVegetableOnPlate;
        player.playerInteraction.onPickKeyPressed += PlayerPickedVegetableFromPlate;
        
        if (player.AreAllHandsFree())
        {
            if(!isEmpty)
            {
                EnableOrDisableInteractionButton(true, player.inputConfig.pickKey.ToString());
            }
        } 
        else
        {
            if (isEmpty)
            {
                EnableOrDisableInteractionButton(true, player.inputConfig.putKey.ToString());
            }
        }
    }

    public override void PlayerLeft(Player player)
    {
        player.playerInteraction.onPutKeyPressed -= PlayerPlacedVegetableOnPlate;
        player.playerInteraction.onPickKeyPressed -= PlayerPickedVegetableFromPlate;

        EnableOrDisableInteractionButton(false);
    }

    void PlayerPlacedVegetableOnPlate(Player player)
    {
        if(player.AreAllHandsFree() && isEmpty)
        {
            return;
        }

        if(isEmpty)
        {
            isEmpty = false;
            vegetableID = player.GetFirstPickedVegetable();
            player.playerInteraction.KeepVegetableOnExtraPlate(vegetableID);
            EnableOrDisableInteractionButton(true, player.inputConfig.pickKey.ToString());
            vegetableOnPlate.sprite = GameManager._instance.vegInventory.GetVegetable(vegetableID).itemSprite;
            vegetableOnPlate.enabled = true;
        }        
    }

    void PlayerPickedVegetableFromPlate(Player player)
    {
        if (!isEmpty)
        {
            player.playerInteraction.PickVegetableFromExtraPlate(vegetableID);
            isEmpty = true;
            vegetableID = "";
            EnableOrDisableInteractionButton(true, player.inputConfig.putKey.ToString());
            vegetableOnPlate.sprite = null;
            vegetableOnPlate.enabled = false;
        }        
    }
}
