using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoppingBoardStateMachine : StateMachine
{   
    public enum CHOPPING_BOARD_STATE
    {
        IDLE,
        CHOPPING,
        SALADREADY
    }
    public float totalChoppingTime;    
    public float timer;
    public CHOPPING_BOARD_STATE choppingBoardCurrentState = CHOPPING_BOARD_STATE.IDLE;

    public GameObject timerObj;
    public Image timerImage;
    
    // Use this for initialization
    void Start ()
    {
        ChangeState(CHOPPING_BOARD_STATE.IDLE);
    }

    public void ChangeState(CHOPPING_BOARD_STATE state)
    {
        switch(state)
        {
            case CHOPPING_BOARD_STATE.IDLE:
                SetState(new ChoppingBoardStateIdle(),this);
                break;
            case CHOPPING_BOARD_STATE.CHOPPING:
                SetState(new ChoppingBoardStateChopping(),this);
                break;
            case CHOPPING_BOARD_STATE.SALADREADY:
                SetState(new ChoppingBoardStateSaladReady(),this);
                break;
        }
    }
    
    public void SetCurrentState(CHOPPING_BOARD_STATE state)
    {
        choppingBoardCurrentState = state;

        if(choppingBoardCurrentState == CHOPPING_BOARD_STATE.SALADREADY)
        {
            ChoppingBoard choppingBoard = GetComponent<ChoppingBoard>();         

            if (choppingBoard.CheckIfSaladReadyToServe(choppingBoard.GetCurrentLockedPlayer()))
            {
                choppingBoard.EnableOrDisableInteractionButton(choppingBoard.pickSaladButton, 
                    true, choppingBoard.GetCurrentLockedPlayer().inputConfig.pickKey.ToString());
            }
            else
            {
                if(!choppingBoard.GetCurrentLockedPlayer().AreAllHandsFree())
                {
                    choppingBoard.EnableOrDisableInteractionButton(true,choppingBoard.GetCurrentLockedPlayer().inputConfig.putKey.ToString());
                }
            }
            
            choppingBoard.FreeUpPlayer();
        }
    }
}
