using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatorIdleState : State
{
    private Gator gator;

    public GatorIdleState(Gator gator, StateMachine stateMachine, string animBoolName) : base(gator, stateMachine, animBoolName)
    {
        this.gator = gator;
    }

    public override void Enter() 
    {
        base.Enter();

        this.stateTimer = gator.ClosedMouthDuration;
        gator.GetComponentInChildren<GatorMouth>().IsDeadly = false;
    }
    
    public override void Update() 
    {
        base.Update();

        if (this.stateTimer <= 0)
        {
            this.stateMachine.ChangeState(gator.OpenMouthState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
