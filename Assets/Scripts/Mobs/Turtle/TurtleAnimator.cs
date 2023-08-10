using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAnimator : MonoBehaviour
{
    public void OnAnimationFinished()
    {
        Turtle turtle = GetComponentInParent<Turtle>();
        turtle.StateMachine.CurrentState.AnimationFinished();
    }

    public void OnTurtleUnderwater()
    {
        Turtle turtle = GetComponentInParent<Turtle>();
        TurtleDiveState diveState = turtle.StateMachine.CurrentState as TurtleDiveState;
        diveState.TurtleUnderwater();
    }
}
