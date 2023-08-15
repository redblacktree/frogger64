using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatorOpenMouthState : State
{
    private Gator gator;
    
    public GatorOpenMouthState(Gator gator, StateMachine stateMachine, string animBoolName) : base(gator, stateMachine, animBoolName)
    {
        this.gator = gator;
    }

    public override void Enter() 
    {
        base.Enter();

        gator.GetComponentInChildren<GatorMouth>().IsDeadly = true;
        this.stateTimer = gator.OpenMouthDuration;
    }
    
    public override void Update() 
    {
        base.Update();

        if (this.stateTimer <= 0)
        {
            this.stateMachine.ChangeState(gator.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
