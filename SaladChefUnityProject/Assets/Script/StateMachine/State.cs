using UnityEngine;
public abstract class State
{
    private StateMachine currentStateMachine;
    public void SetCurrentStateMachine(StateMachine stateMachine)
    {
        currentStateMachine = stateMachine;
    }

    public StateMachine GetCurrentStateMachine()
    {
        return currentStateMachine;
    }

    public abstract void Execute();
    public virtual void Enter() { }
    public virtual void Exit() { }    
}
