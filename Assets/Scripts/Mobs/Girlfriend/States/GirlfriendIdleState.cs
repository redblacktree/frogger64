using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlfriendIdleState : State
{
    private Girlfriend girlfriend;
    private float idleTime = 2f;

    public GirlfriendIdleState(Girlfriend girlfriend, StateMachine stateMachine, string animBoolName) : base(girlfriend, stateMachine, animBoolName)
    {
        this.girlfriend = girlfriend;
    }

    public override void Enter() 
    {
        base.Enter();

        if (girlfriend.transform.localPosition.x < 0 && girlfriend.FacingDirection == Vector2.left)
        {
            girlfriend.StartCoroutine(TurnCoroutine());
        }
        else if (girlfriend.transform.localPosition.x > 0 && girlfriend.FacingDirection == Vector2.right)
        {
            girlfriend.StartCoroutine(TurnCoroutine());
        }
        else
        {
            girlfriend.StartCoroutine(JumpCoroutine());
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

    private IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(idleTime);
        stateMachine.ChangeState(girlfriend.JumpState);
    }

    private IEnumerator TurnCoroutine()
    {
        yield return new WaitForSeconds(idleTime);
        stateMachine.ChangeState(girlfriend.TurnState);
    }
}
