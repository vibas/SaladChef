
public class CustomerStateSatisfied : State
{
    CustomerStateMachine machine;
    public override void Enter()
    {
        base.Enter();
        machine = (CustomerStateMachine)GetCurrentStateMachine();
        machine.SetCurrentState(CustomerStateMachine.CUSTOMER_STATE.SATISFIED);

        if (machine.timer / machine.totalWatingTime < 0.8f)
        {
            machine.currentCustomer.GetSatisfied(true);
        }
        else
        {
            machine.currentCustomer.GetSatisfied(false);
        }

        machine.timerObj.SetActive(false);        
    }

    float timer = 0;
    public override void Execute()
    {
        timer += UnityEngine.Time.deltaTime;
        if(timer>=machine.waitTimeBeforeLeaving)
        {
            machine.ChangeState(CustomerStateMachine.CUSTOMER_STATE.LEAVE);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
