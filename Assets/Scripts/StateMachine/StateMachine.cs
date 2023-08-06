using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public virtual State CurrentState { get; private set; }

    public virtual void Initialize(State startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public virtual void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}