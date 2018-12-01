using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCounter : InteractibleKitchenElement
{
    public bool isOccupied;
    public Customer customer;  

    public void OnCustomerLeft()
    {
        customer = null;
        isOccupied = false;
    }

    public override void PlayerReached(Player player)
    {
        base.PlayerReached(player);        
        player.playerInteraction.onPutKeyPressed += DeliverSaladToCustomer;        

        // If Player is holding any vegetable
        if(player.IsCarryingSalad())
            EnableOrDisableInteractionButton(true, player.inputConfig.putKey.ToString());        
    }

    public override void PlayerLeft(Player player)
    {
        base.PlayerLeft(player);
        player.playerInteraction.onPutKeyPressed -= DeliverSaladToCustomer;        

        EnableOrDisableInteractionButton(false);        
    }

    public void DeliverSaladToCustomer(Player player)
    {
        player.currentSalad.Sort();
        customer.orderSalad.Sort();

        if(Utility.AreBothListEqual(player.currentSalad,customer.orderSalad))
        {
            customer.GetComponent<CustomerStateMachine>().ChangeState(CustomerStateMachine.CUSTOMER_STATE.SATISFIED);
        }
        else
        {
            customer.GetComponent<CustomerStateMachine>().ChangeState(CustomerStateMachine.CUSTOMER_STATE.ANGRY);
        }

        player.RemoveSaladFromHand();
        EnableOrDisableInteractionButton(false);        
    }    
}
