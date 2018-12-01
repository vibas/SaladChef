using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : InteractibleKitchenElement
{
    public override void PlayerReached(Player player)
    {
        player.playerInteraction.onPutKeyPressed += PlayerDumpedSalad;
        EnableOrDisableInteractionButton(true, player.inputConfig.pickKey.ToString());        
    }

    public override void PlayerLeft(Player player)
    {
        player.playerInteraction.onPickKeyPressed -= PlayerDumpedSalad;
        EnableOrDisableInteractionButton(false);
    }

    void PlayerDumpedSalad(Player player)
    {    
        
    }

}
