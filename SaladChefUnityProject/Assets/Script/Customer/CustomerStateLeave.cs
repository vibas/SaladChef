﻿public class CustomerStateLeave : State
{
    CustomerStateMachine machine;
    public override void Enter()
    {
        base.Enter();
        machine = (CustomerStateMachine)GetCurrentStateMachine();
        machine.SetCurrentState(CustomerStateMachine.CUSTOMER_STATE.LEAVE);
        machine.currentCustomer.LeaveCounter();
    }

    public override void Execute()
    {        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
