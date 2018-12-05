/// <summary>
/// Attached to each vegetable box
/// Holds respective data and handles player's interaction (can pick)  
/// </summary>
public class VegetableBox : InteractibleKitchenElement
{
    public string itemID;      // Vegetable ID

    public override void PlayerReached(Player player)
    {
        player.playerInteraction.onPickKeyPressed += PlayerPickedVegetable;
        if(player.IsAnyHandFree() && !player.IsCarryingSalad())
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
        if (player.IsAnyHandFree() && !player.IsCarryingSalad())
        {
            player.playerInteraction.PickVegetable(itemID);
            EnableOrDisableInteractionButton(player.IsAnyHandFree(), player.inputConfig.pickKey.ToString());
        }        
    }   
}
