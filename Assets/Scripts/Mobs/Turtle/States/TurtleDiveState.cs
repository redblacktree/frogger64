using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleDiveState : State
{
    private Turtle turtle;

    public TurtleDiveState(Turtle turtle, StateMachine stateMachine, string animBoolName) : base(turtle, stateMachine, animBoolName)
    {
        this.turtle = turtle;
    }

    public override void Enter() 
    {
        base.Enter();        
    }
    
    public override void Update() 
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();

        turtle.col.enabled = true;
    }

    public void TurtleUnderwater()
    {
        turtle.col.enabled = false;
    }

    public override void AnimationFinished()
    {
        base.AnimationFinished();

        turtle.StateMachine.ChangeState(turtle.idleState);
    }
}
