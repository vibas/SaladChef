using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCounter : InteractibleKitchenElement
{
    public bool isOccupied;
    public Customer customer;
    Player currentPlayer;

    public void OnCustomerLeft()
    {
        customer = null;
        isOccupied = false;
        this.gameObject.SetActive(false);
    }

    public override void PlayerReached(Player player)
    {
        base.PlayerReached(player);
        currentPlayer = player;
        player.playerInteraction.onPutKeyPressed += DeliverSaladToCustomer;        

        // If Player is holding any vegetable
        if(player.IsCarryingSalad())
            EnableOrDisableInteractionButton(true, player.inputConfig.putKey.ToString());        
    }

    public override void PlayerLeft(Player player)
    {
        base.PlayerLeft(player);
        currentPlayer = null;
        player.playerInteraction.onPutKeyPressed -= DeliverSaladToCustomer;        

        EnableOrDisableInteractionButton(false);        
    }

    public void DeliverSaladToCustomer(Player player)
    {
        player.currentSalad.ingredientsList.Sort();
        customer.orderSalad.ingredientsList.Sort();
        customer.playerWhoDeliveredSalad = player;

        if(Utility.AreBothListEqual(player.currentSalad.ingredientsList,customer.orderSalad.ingredientsList))
        {
            customer.GetComponent<CustomerStateMachine>().ChangeState(CustomerStateMachine.CUSTOMER_STATE.SATISFIED);
        }
        else
        {
            customer.GetComponent<CustomerStateMachine>().ChangeState(CustomerStateMachine.CUSTOMER_STATE.ANGRY);
        }

        player.playerInteraction.RemoveSaladFromHand();
        EnableOrDisableInteractionButton(false);        
    }

    private void OnDisable()
    {
        if(currentPlayer!=null)
        {
            currentPlayer.playerInteraction.onPutKeyPressed -= DeliverSaladToCustomer;            
        }
    }
}
