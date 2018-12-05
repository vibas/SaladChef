using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attached to each chopping board to maintain their state and timer
/// </summary>
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
    public bool shouldRunTimer;         // Need to stop/pause timer according to games state

    public CHOPPING_BOARD_STATE choppingBoardCurrentState = CHOPPING_BOARD_STATE.IDLE;

    public GameObject timerObj;
    public Image timerImage;        // Timer Progress Image
    
    public void InitStateMachine ()
    {
        ChangeState(CHOPPING_BOARD_STATE.IDLE);
        shouldRunTimer = true;
    }

    public void ResetTimer()
    {
        timerObj.SetActive(false);
        shouldRunTimer = false;
        timer = 0;
    }

    public void PauseTimer()
    {
        shouldRunTimer = false;
    }

    public void ResumeTimer()
    {
        shouldRunTimer = true;
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
            
            if(!GameManager._instance.isGameOver && !GameManager._instance.isGamePaused)
            {
                choppingBoard.FreeUpPlayer();
            }                
        }
    }
}
