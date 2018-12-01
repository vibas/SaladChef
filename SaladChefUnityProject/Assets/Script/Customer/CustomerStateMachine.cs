﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int timerSpeedMultiplier=1;
    public float totalWatingTime;
    public float timer;
    public CUSTOMER_STATE customerCurrentState = CUSTOMER_STATE.WAITING;

    public GameObject timerObj;
    public Image timerImage;

    // Use this for initialization
    void Start () {
        ChangeState(CUSTOMER_STATE.WAITING);
    }

    public void ChangeState(CUSTOMER_STATE state)
    {
        switch (state)
        {
            case CUSTOMER_STATE.WAITING:
                SetState(new CustomerStateWaiting(), this);
                break;
            case CUSTOMER_STATE.SATISFIED:
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

        if(customerCurrentState == CUSTOMER_STATE.LEAVE)
        {
            GetComponent<Customer>().counter.OnCustomerLeft();
            Destroy(this.gameObject);
        }
    }
}