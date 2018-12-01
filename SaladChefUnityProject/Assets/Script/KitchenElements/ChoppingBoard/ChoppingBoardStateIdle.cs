public class ChoppingBoardStateIdle : State
{
    ChoppingBoardStateMachine machine;
    public override void Enter()
    {
        base.Enter();
        machine = (ChoppingBoardStateMachine) GetCurrentStateMachine();
        machine.SetCurrentState(ChoppingBoardStateMachine.CHOPPING_BOARD_STATE.IDLE);        
    }

    public override void Execute()
    {
       
    }

    public override void Exit()
    {
        base.Exit();
    }
}
