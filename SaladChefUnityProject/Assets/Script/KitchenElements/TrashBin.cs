/// <summary>
/// Attached to Trash Bin GameObject
/// </summary>
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
            GameManager._instance.playerRewardSystemInstance.PunishPlayerWithScore(player, player.currentSalad.ingredientsList.Count * 2);
            player.playerInteraction.RemoveSaladFromHand();
            EnableOrDisableInteractionButton(false);
        }
    }

}
