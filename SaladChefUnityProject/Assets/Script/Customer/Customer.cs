using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Salad orderSalad;

    public GameObject itemPrefab;
    public Transform content;
    public CustomerCounter counter;

    public Player playerWhoDeliveredSalad;

    public bool isAngry,isSatisfied,isImpressed, isDissatisfied;

    [SerializeField]
    private Sprite[] faceReactionSprite;
    [SerializeField]
    private SpriteRenderer customerFace;

    public CustomerStateMachine customerStateMachine;

    public void CheckForMenu()
    {
        orderSalad = GameManager._instance.saladMeuManagerInstance.GetRandomSaladFromMenu();
        DisplayOrderItemIngredients();
        customerStateMachine = GetComponent<CustomerStateMachine>();
        customerStateMachine.SetTotalWaitingTimer(orderSalad.ingredientsList.Count);
    }

    void DisplayOrderItemIngredients()
    {
        for (int i = 0; i < orderSalad.ingredientsList.Count; i++)
        {
            GameObject vegItem = Instantiate(itemPrefab) as GameObject;
            vegItem.transform.SetParent(content);
            vegItem.transform.localScale = Vector3.one;
            vegItem.transform.localPosition = Vector3.zero;
            vegItem.GetComponent<Image>().sprite = GameManager._instance.vegInventory.GetVegetable(orderSalad.ingredientsList[i]).itemSprite;
        }
    }

    public void GetSatisfied(bool isImpressed = false)
    {
        isSatisfied = true;
        this.isImpressed = isImpressed;       

        if(isAngry)
        {
            isAngry = false;
        }

        customerFace.sprite = faceReactionSprite[(int)CustomerStateMachine.CUSTOMER_STATE.SATISFIED];
    }

    public void GetDissatisfied()
    {
        customerFace.sprite = faceReactionSprite[(int)CustomerStateMachine.CUSTOMER_STATE.DISSATISFIED];
        isDissatisfied = true;
    }

    public void GetWorried()
    {
        customerFace.sprite = faceReactionSprite[4]; // Worried Face index
    }

    public void GetAngry()
    {
        isAngry = true;
        customerFace.sprite = faceReactionSprite[(int)CustomerStateMachine.CUSTOMER_STATE.ANGRY];
    }  

    public void LeaveCounter()
    {
        RewardOrPunishPlayer();
        counter.OnCustomerLeft();
        GameManager._instance.customerManagerInstance.RemoveCustomerFromAllCustomer(this);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Before Customer Leaves the counter, he/she gives some reward or punishes the player.
    /// </summary>
    void RewardOrPunishPlayer()
    {        
        if (isAngry)
        {
            GameManager._instance.playerRewardSystemInstance.PunishPlayerWithScore(playerWhoDeliveredSalad, GameManager._instance.gameConfig.angryCustomerPenalty);            
        }
        else if (isSatisfied)
        {
            GameManager._instance.playerRewardSystemInstance.RewardPlayerWithScore(playerWhoDeliveredSalad, orderSalad.price);            
            if (isImpressed)
            {
                GameManager._instance.playerRewardSystemInstance.RewardPlayerWithPowerUp(playerWhoDeliveredSalad);
            }            
        } 
        else if(isDissatisfied)
        {
            GameManager._instance.playerRewardSystemInstance.PunishBothPlayerWithScore(GameManager._instance.gameConfig.dissatisfiedCustomerPenalty);
        }
    }
}

