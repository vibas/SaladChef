public class CustomerStateDissatisfied : State
{

    CustomerStateMachine machine;
    public override void Enter()
    {
        base.Enter();
        machine = (CustomerStateMachine)GetCurrentStateMachine();
        machine.SetCurrentState(CustomerStateMachine.CUSTOMER_STATE.DISSATISFIED);
        machine.currentCustomer.GetDissatisfied();
    }

    float timer = 0;
    public override void Execute()
    {
        timer += UnityEngine.Time.deltaTime;
        if (timer >= machine.waitTimeBeforeLeaving)
        {
            machine.ChangeState(CustomerStateMachine.CUSTOMER_STATE.LEAVE);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
