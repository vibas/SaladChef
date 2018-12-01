
public class CustomerStateSatisfied : State {

    CustomerStateMachine machine;
    public override void Enter()
    {
        base.Enter();
        machine = (CustomerStateMachine)GetCurrentStateMachine();
        machine.SetCurrentState(CustomerStateMachine.CUSTOMER_STATE.SATISFIED);
        machine.ChangeState(CustomerStateMachine.CUSTOMER_STATE.LEAVE);
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        base.Exit();
    }
}
