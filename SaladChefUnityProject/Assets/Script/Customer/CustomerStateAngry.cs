﻿public class CustomerStateAngry : State
{
    CustomerStateMachine machine;
    public override void Enter()
    {
        base.Enter();
        machine = (CustomerStateMachine)GetCurrentStateMachine();
        machine.SetCurrentState(CustomerStateMachine.CUSTOMER_STATE.ANGRY);
        machine.currentCustomer.GetAngry();
        machine.RunTimerFaster();
        machine.ChangeState(CustomerStateMachine.CUSTOMER_STATE.WAITING);
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        base.Exit();
    }
}
