using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girlfriend : Entity
{
    public GirlfriendStateMachine StateMachine { get; private set; }
        
    public GirlfriendIdleState IdleState { get; private set; }
    public GirlfriendJumpState JumpState { get; private set; }
    public GirlfriendTurnState TurnState { get; private set; }
    public GirlfriendRidingState RidingState { get; private set; }

    public float JumpSpeed = 5f;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new GirlfriendStateMachine();
        IdleState = new GirlfriendIdleState(this, StateMachine, "Idle");    
        JumpState = new GirlfriendJumpState(this, StateMachine, "Jump");
        TurnState = new GirlfriendTurnState(this, StateMachine, "Idle");        
        RidingState = new GirlfriendRidingState(this, StateMachine, "Idle");
    }

    protected override void Start()
    {
        base.Start();
        
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.Update();
    }

    public Vector2 FacingDirection => transform.rotation.eulerAngles.z == 0 ? Vector2.up : transform.rotation.eulerAngles.z == 180 ? Vector2.down : transform.rotation.eulerAngles.z == 90 ? Vector2.left : Vector2.right;

    public void FaceDirection(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);            
        }
        else if (direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);            
        }
        else if (direction == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);            
        }
    }

    private void HopOnPlayersBack(GameObject player)
    {
        transform.parent = player.transform;
        transform.localPosition = new Vector3(0, -0.125f, 0);
        transform.localRotation = player.transform.rotation;
        StopAllCoroutines();
        StateMachine.ChangeState(RidingState);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            HopOnPlayersBack(other.gameObject);
        }
    }
}
