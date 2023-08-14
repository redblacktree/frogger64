using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girlfriend : Entity
{
    public GirlfriendStateMachine StateMachine { get; private set; }
        
    public GirlfriendIdleState IdleState { get; private set; }
    public GirlfriendJumpState JumpState { get; private set; }
    public GirlfriendTurnState TurnState { get; private set; }

    public float JumpSpeed = 5f;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new GirlfriendStateMachine();
        IdleState = new GirlfriendIdleState(this, StateMachine, "Idle");    
        JumpState = new GirlfriendJumpState(this, StateMachine, "Jump");
        TurnState = new GirlfriendTurnState(this, StateMachine, "Turn");        
    }

    protected override void Start()
    {
        base.Start();
        Debug.Log("Girlfriend Start");
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();        
    }

    public Vector2 FacingDirection => transform.rotation.eulerAngles.z == 0 ? Vector2.up : transform.rotation.eulerAngles.z == 180 ? Vector2.down : transform.rotation.eulerAngles.z == 90 ? Vector2.left : Vector2.right;

    public void FaceDirection(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            transform.Rotate(0, 0, 0);
        }
        else if (direction == Vector2.down)
        {
            transform.Rotate(0, 0, 180);
        }
        else if (direction == Vector2.left)
        {
            transform.Rotate(0, 0, 90);
        }
        else if (direction == Vector2.right)
        {
            transform.Rotate(0, 0, -90);
        }
    }
}
