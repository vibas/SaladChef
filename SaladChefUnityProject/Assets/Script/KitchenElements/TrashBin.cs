using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : InteractibleKitchenElement
{
    public override void PlayerReached(Player player)
    {
        player.playerInteraction.onPutKeyPressed += PlayerDumpedSalad;

        if(player.IsCarryingSalad())
            EnableOrDisableInteractionButton(true, player.inputConfig.putKey.ToString());        
    }

    public override void PlayerLeft(Player player)
    {
        player.playerInteraction.onPutKeyPressed -= PlayerDumpedSalad;
        EnableOrDisableInteractionButton(false);
    }

    void PlayerDumpedSalad(Player player)
    {    
        if(player.IsCarryingSalad())
        {
            player.playerInteraction.RemoveSaladFromHand();
            EnableOrDisableInteractionButton(false);
        }
    }

}
