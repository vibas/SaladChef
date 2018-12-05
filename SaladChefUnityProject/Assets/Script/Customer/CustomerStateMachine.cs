using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Customer State Machine attached to Customer Game Object 
/// Used to check and modify customer's state and perform each state's action individually
/// </summary>
public class CustomerStateMachine : StateMachine
{
    public enum CUSTOMER_STATE
    {
        WAITING,
        SATISFIED,
        DISSATISFIED,
        ANGRY,
        LEAVE
    }
    
    public float waitTimeBeforeLeaving;
    public int timerSpeedMultiplier=1;      // This variable increases if customer gets angry, so timer runs faster
    public float totalWatingTime;
    public float timer;

    public CUSTOMER_STATE customerCurrentState = CUSTOMER_STATE.WAITING;

    public GameObject timerObj;             // Timer Indicator
    public Image timerImage;                // Image to show timer progress

    public Customer currentCustomer;

    public bool shouldRunTimer = true;      // According to Game state we need to stop customer's timer 

    /// <summary>
    /// Checks the total number of ingredients and sets total waitig timer
    /// </summary>
    /// <param name="ingredientCount"></param>
    public void SetTotalWaitingTimer(int ingredientCount)
    {
        totalWatingTime = ingredientCount * GameManager._instance.gameConfig.vegChoppingTimerFactor;
    }
    
    void Start ()
    {        
        ChangeState(CUSTOMER_STATE.WAITING);
        currentCustomer = GetComponent<Customer>();
        waitTimeBeforeLeaving = GameManager._instance.gameConfig.waitTimeBeforeLeaving;
        shouldRunTimer = true;
    }

    public void ChangeState(CUSTOMER_STATE state)
    {
        switch (state)
        {
            case CUSTOMER_STATE.WAITING:
                SetState(new CustomerStateWaiting(), this);
                break;
            case CUSTOMER_STATE.SATISFIED:
                SetState(new CustomerStateSatisfied(), this);
                break;
            case CUSTOMER_STATE.DISSATISFIED:
                SetState(new CustomerStateDissatisfied(), this);
                break;
            case CUSTOMER_STATE.ANGRY:
                SetState(new CustomerStateAngry(), this);
                break;
            case CUSTOMER_STATE.LEAVE:
                SetState(new CustomerStateLeave(), this);
                break;
        }
    }

    public void SetCurrentState(CUSTOMER_STATE state)
    {
        customerCurrentState = state;  
    }

    /// <summary>
    /// Angry customer's timer runs faster
    /// </summary>
    public void RunTimerFaster()
    {
        timerSpeedMultiplier = GameManager._instance.gameConfig.angryCustomerTimerMultiplier;
    }
}
