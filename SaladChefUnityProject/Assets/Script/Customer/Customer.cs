using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public List<string> orderSalad;

    public GameObject itemPrefab;
    public Transform content;
    public CustomerCounter counter;

    public Player playerWhoDeliveredSalad;

    public bool isAngry,isSatisfied,isImpressed;

    [SerializeField]
    private Sprite[] faceReactionSprite;
    [SerializeField]
    private SpriteRenderer customerFace;

    public void CheckForMenu()
    {
        orderSalad = GameManager._instance.saladMeuManager.GetRandomSaladFromMenu();
        DisplayOrderItemIngredients();
        GetComponent<CustomerStateMachine>().SetTotalWaitingTimer(orderSalad.Count);
    }

    void DisplayOrderItemIngredients()
    {
        for (int i = 0; i < orderSalad.Count; i++)
        {
            GameObject vegItem = Instantiate(itemPrefab) as GameObject;
            vegItem.transform.SetParent(content);
            vegItem.transform.localScale = Vector3.one;
            vegItem.transform.localPosition = Vector3.zero;
            vegItem.GetComponent<Image>().sprite = GameManager._instance.vegInventory.GetVegetable(orderSalad[i]).itemSprite;
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
    }

    public void GetWorried()
    {
        customerFace.sprite = faceReactionSprite[4];
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
        Destroy(this.gameObject);
    }

    void RewardOrPunishPlayer()
    {
        if (isAngry)
        {
            
        }
        else if (isSatisfied)
        {
            if (isImpressed)
            {

            }
            else
            {

            }
        }
    }
}

