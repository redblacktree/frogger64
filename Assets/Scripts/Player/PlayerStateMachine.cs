using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public override State CurrentState => base.CurrentState;

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

    public override void Initialize(State startState)
    {
        base.Initialize(startState);
    }
}
