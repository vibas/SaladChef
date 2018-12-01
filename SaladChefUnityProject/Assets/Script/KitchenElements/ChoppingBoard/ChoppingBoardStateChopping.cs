public class ChoppingBoardStateChopping : State
{
    ChoppingBoardStateMachine machine;
    public override void Enter()
    {
        base.Enter();
        machine = (ChoppingBoardStateMachine)GetCurrentStateMachine();
        machine.SetCurrentState(ChoppingBoardStateMachine.CHOPPING_BOARD_STATE.CHOPPING);
    }

    public override void Execute()
    {
        machine.timer += UnityEngine.Time.deltaTime;
        if(machine.timer>=machine.totalChoppingTime)
        {
            machine.timer = 0;
            machine.ChangeState(ChoppingBoardStateMachine.CHOPPING_BOARD_STATE.SALADREADY);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
