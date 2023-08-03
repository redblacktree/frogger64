using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public float moveSpeed = 5f;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public InputActionAsset Input { get; private set; }
    public Vector2 MoveInput { get; private set; }

    public GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();

        Input = Resources.Load<InputActionAsset>("InputActions");
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");        
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

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public Vector2 GridPosition => gameManager.map.WorldPointToGridCoord(transform.position);
}
