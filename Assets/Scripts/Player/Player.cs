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
    public PlayerDeathState DeathState { get; private set; }
    public PlayerHomeState HomeState { get; private set; }

    public InputActionAsset Input { get; private set; }
    public Vector2 MoveInput { get; private set; }

    public GameManager gameManager;
    public bool HomeSafe = false;
    public bool Riding = false;

    protected override void Awake()
    {
        base.Awake();

        Input = Resources.Load<InputActionAsset>("InputActions");
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        DeathState = new PlayerDeathState(this, StateMachine, "Death");
        HomeState = new PlayerHomeState(this, StateMachine, "Home");
        gameManager = GameManager.Instance;
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

    public void Die()
    {
        StateMachine.ChangeState(DeathState);
    }

    public void Home()
    {
        StateMachine.ChangeState(HomeState);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        Vector2 moveDirection = Vector2.zero;

        // find the maximum input value over 0.5 and set direction to that
        if (Mathf.Abs(MoveInput.x) > 0.5f)
        {
            moveDirection.x = Mathf.Sign(MoveInput.x);
        }
        else if (Mathf.Abs(MoveInput.y) > 0.5f)
        {
            moveDirection.y = Mathf.Sign(MoveInput.y);
        }
        MoveState.Direction = moveDirection;

        if (StateMachine.CurrentState == IdleState
            && moveDirection != Vector2.zero)
        {
            StateMachine.ChangeState(MoveState);
        }
    }

    public void OnAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }
}
