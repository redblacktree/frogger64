using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gator : Platform
{
    public GatorStateMachine StateMachine { get; private set; }

    public GatorIdleState IdleState { get; private set; }
    public GatorOpenMouthState OpenMouthState { get; private set; }

    public float OpenMouthDuration = 2f;
    public float ClosedMouthDuration = 3f;

    protected override void Start()
    {
        base.Start();

        StateMachine = new GatorStateMachine();

        IdleState = new GatorIdleState(this, StateMachine, "Idle");
        OpenMouthState = new GatorOpenMouthState(this, StateMachine, "OpenMouth");

        if (MoveDirection == -1)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.Update();
    }
}
