using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to player Game Object
/// All interaction happens here.
/// Also player's input for interaction is handled in this class
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    public delegate void OnPickKeyPressed(Player p);
    public OnPickKeyPressed onPickKeyPressed;

    public delegate void OnPutKeyPressed(Player p);
    public OnPutKeyPressed onPutKeyPressed;

    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    #region PlayerInteraction
    
    /// <summary>
    /// Picks a vegetable from basket
    /// </summary>
    /// <param name="itemID"></param>
    public void PickVegetable(string itemID)
    {
        player.AddItemToPlayerHand(itemID);
    }

    public void KeepVegetableOnExtraPlate(string itemID)
    {
        player.RemoveItemFromHand(itemID);
    }

    public void PickVegetableFromExtraPlate(string itemID)
    {
        player.AddItemToPlayerHand(itemID);
    }

    /// <summary>
    /// Puts the vegetable on chopping board and starts chopping
    /// </summary>
    /// <param name="itemID"></param>
    public void StartChoppingVegetable(string itemID)
    {
        player.RemoveItemFromHand(itemID);
        player.LockOrUnlockPlayerMovement(true);
    }    

    /// <summary>
    /// Once a salad is ready, player carrys the salad to serve
    /// </summary>
    /// <param name="preparedSalad"></param>
    public void CarryPreparedSalad(List<string> preparedSalad)
    {
        player.currentSalad.ingredientsList = preparedSalad;
        player.isPlayerCarryingSalad = true;
        player.ShowOrHideSaladInPlayerHand(true);
    }

    /// <summary>
    /// Once player delivers salad to customer or dumps in trash,
    /// its removed from player's hand
    /// </summary>
    public void RemoveSaladFromHand()
    {
        player.currentSalad.ingredientsList.Clear();
        player.isPlayerCarryingSalad = false;
        player.ShowOrHideSaladInPlayerHand(false);
    }

    #endregion Player Interation

    private void Update()
    {
        if(GameManager._instance.isGameOver || GameManager._instance.isGamePaused)
        {
            return;
        }

        if(Input.GetKeyDown(player.inputConfig.pickKey))
        {
            if(onPickKeyPressed!=null)
                onPickKeyPressed(player);
        }

        if(Input.GetKeyDown(player.inputConfig.putKey))
        {
            if(onPutKeyPressed!=null)
                onPutKeyPressed(player);
        }
    }
}
