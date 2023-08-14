using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleIdleState : State
{
    private Turtle turtle;
    private float FloatTime = 5f;

    private bool timeToDive = false;

    public TurtleIdleState(Turtle turtle, StateMachine stateMachine, string animBoolName) : base(turtle, stateMachine, animBoolName)
    {
        this.turtle = turtle;
    }

    public override void Enter() 
    {
        base.Enter();
        timeToDive = false;
        if (turtle.Dives)
        {
            turtle.StartCoroutine(FloatCoroutine());
        }
    }
    
    public override void Update() 
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();        
    }

    private IEnumerator FloatCoroutine()
    {
        yield return new WaitForSeconds(FloatTime);
        timeToDive = true;
    }

    public override void AnimationFinished()
    {
        base.AnimationFinished();        

        if (timeToDive)
        {
            turtle.StateMachine.ChangeState(turtle.diveState);
        }
    }
}
