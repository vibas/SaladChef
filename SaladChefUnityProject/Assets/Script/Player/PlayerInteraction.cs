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

    public void PickVegetable(string itemID)
    {
        player.AddItemToPlayerHand(itemID);
    }

    public void StartChoppingVegetable(string itemID)
    {
        player.RemoveItemFromHand(itemID);
        player.LockOrUnlockPlayerMovement(true);
    }

    public void StopChoppingVegetable()
    {
        player.LockOrUnlockPlayerMovement(false);
    }  

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
