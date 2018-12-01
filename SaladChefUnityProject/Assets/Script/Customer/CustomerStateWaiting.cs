public class CustomerStateWaiting : State
{
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
        machine.timer += UnityEngine.Time.deltaTime*machine.timerSpeedMultiplier;
        machine.timerImage.fillAmount =1-(machine.timer / machine.totalWatingTime);
        if (machine.timer >= machine.totalWatingTime)
        {
            machine.timerObj.SetActive(false);
            machine.timer = 0;
            machine.ChangeState(CustomerStateMachine.CUSTOMER_STATE.DISSATISFIED);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
