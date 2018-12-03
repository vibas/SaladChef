public class CustomerStateWaiting : State
{
    bool hasCustomerStartedWorrying = false;
    CustomerStateMachine machine;
    public override void Enter()
    {
        base.Enter();
        machine = (CustomerStateMachine)GetCurrentStateMachine();
        machine.SetCurrentState(CustomerStateMachine.CUSTOMER_STATE.WAITING);
        machine.timerObj.SetActive(true);
    }

    public override void Execute()
    {
        if(machine.shouldRunTimer)
        {
            machine.timer += UnityEngine.Time.deltaTime * machine.timerSpeedMultiplier;
            machine.timerImage.fillAmount = 1 - (machine.timer / machine.totalWatingTime);
        }        
        
        if (machine.timer >= machine.totalWatingTime)
        {
            machine.timerObj.SetActive(false);
            machine.timer = 0;

            if (!machine.currentCustomer.isAngry)
                machine.ChangeState(CustomerStateMachine.CUSTOMER_STATE.DISSATISFIED);
            else
                machine.ChangeState(CustomerStateMachine.CUSTOMER_STATE.LEAVE);
        }
        else
        {
            if (machine.timer / machine.totalWatingTime > 0.8f)
            {
                if(!hasCustomerStartedWorrying && !machine.currentCustomer.isAngry)
                {
                    hasCustomerStartedWorrying = true;
                    machine.currentCustomer.GetWorried();
                }                
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
