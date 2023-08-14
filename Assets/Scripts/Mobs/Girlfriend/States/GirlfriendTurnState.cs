using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlfriendTurnState : State
{
    private Girlfriend girlfriend;
    private float turnTimer = 0.5f;
    private Vector2 previousDirection;

    public GirlfriendTurnState(Girlfriend girlfriend, StateMachine stateMachine, string animBoolName) : base(girlfriend, stateMachine, animBoolName)
    {
        this.girlfriend = girlfriend;
    }

    public override void Enter() 
    {
        base.Enter();

        previousDirection = girlfriend.FacingDirection;
        girlfriend.FaceDirection(Vector2.up);
        girlfriend.StartCoroutine(TurnCoroutine());
    }
    
    public override void Update() 
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private IEnumerator TurnCoroutine()
    {
        yield return new WaitForSeconds(turnTimer);

        if (previousDirection == Vector2.left)
        {
            girlfriend.FaceDirection(Vector2.right);
        }
        else if (previousDirection == Vector2.right)
        {
            girlfriend.FaceDirection(Vector2.left);
        }
    }

    private IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(turnTimer);
        girlfriend.StateMachine.ChangeState(girlfriend.JumpState);
    }
}
