public class ChoppingBoardStateChopping : State
{
    ChoppingBoardStateMachine machine;
    public override void Enter()
    {
        base.Enter();
        machine = (ChoppingBoardStateMachine)GetCurrentStateMachine();
        machine.SetCurrentState(ChoppingBoardStateMachine.CHOPPING_BOARD_STATE.CHOPPING);
        machine.timerObj.SetActive(true);
    }

    public override void Execute()
    {
        machine.timer += UnityEngine.Time.deltaTime;
        machine.timerImage.fillAmount = machine.timer / machine.totalChoppingTime;
        if(machine.timer>=machine.totalChoppingTime)
        {
            machine.timerObj.SetActive(false);
            machine.timer = 0;
            machine.ChangeState(ChoppingBoardStateMachine.CHOPPING_BOARD_STATE.SALADREADY);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
