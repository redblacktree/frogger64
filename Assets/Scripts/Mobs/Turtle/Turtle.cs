using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Platform
{
    public TurtleStateMachine StateMachine { get; private set; }
    public float timeFloating = 5f;
    
    public TurtleIdleState idleState { get; private set; }
    public TurtleDiveState diveState { get; private set; }    


    protected override void Awake()
    {
        base.Awake();
        StateMachine = new TurtleStateMachine();
        idleState = new TurtleIdleState(this, StateMachine, "Idle");
        diveState = new TurtleDiveState(this, StateMachine, "Dive");        
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.Update();
    }
}
