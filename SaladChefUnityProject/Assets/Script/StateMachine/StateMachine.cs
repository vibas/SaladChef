using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    State currentState;    
	// Update is called once per frame
	void Update ()
    {
        if (currentState != null)
            currentState.Execute();
	}   

    public void SetState(State state, StateMachine machine)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = state;
        currentState.SetCurrentStateMachine(machine);       

        if(currentState!=null)
        {
            currentState.Enter();
        }
    }
}
