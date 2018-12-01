using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableBox : InteractibleKitchenElement
{
    public string itemID;      

    public override void PlayerReached(Player player)
    {
        player.playerInteraction.onPickKeyPressed += PlayerPickedVegetable;
        if(player.IsAnyHandFree())
        {
            EnableOrDisableInteractionButton(true, player.inputConfig.pickKey.ToString());
        }        
    }

    public override void PlayerLeft(Player player)
    {
        player.playerInteraction.onPickKeyPressed -= PlayerPickedVegetable;
        EnableOrDisableInteractionButton(false);
    }

    void PlayerPickedVegetable(Player player)
    {
        if (player.IsAnyHandFree())
        {
            player.playerInteraction.PickVegetable(itemID);
            EnableOrDisableInteractionButton(player.IsAnyHandFree(), player.inputConfig.pickKey.ToString());
        }        
    }   
}
