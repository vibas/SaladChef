using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// After Chopping a vegetable, player needs to be free to move
    /// </summary>
    public void StopChoppingVegetable()
    {
        player.LockOrUnlockPlayerMovement(false);
    }

    /// <summary>
    /// Once a salad is ready, player carrys the salad to serve
    /// </summary>
    /// <param name="preparedSalad"></param>
    public void CarryPreparedSalad(List<string> preparedSalad)
    {
        player.currentSalad = preparedSalad;
        player.isPlayerCarryingSalad = true;
        player.ShowOrHideSaladInPlayerHand(true);
    }

    /// <summary>
    /// Once player delivers salad to customer or dumps in trash,
    /// its removed from player's hand
    /// </summary>
    public void RemoveSaladFromHand()
    {
        player.currentSalad.Clear();
        player.isPlayerCarryingSalad = false;
        player.ShowOrHideSaladInPlayerHand(false);
    }

    #endregion Player Interation

    private void Update()
    {
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
