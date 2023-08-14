using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlfriendJumpState : State
{
    private Girlfriend girlfriend;
    private Vector2 TargetPosition;

    public GirlfriendJumpState(Girlfriend girlfriend, StateMachine stateMachine, string animBoolName) : base(girlfriend, stateMachine, animBoolName)
    {
        this.girlfriend = girlfriend;
    }

    public override void Enter() 
    {
        base.Enter();

        TargetPosition = (Vector2)girlfriend.transform.localPosition + girlfriend.FacingDirection;
    }
    
    public override void Update() 
    {
        base.Update();

        if (Vector2.Distance(girlfriend.transform.localPosition, TargetPosition) > 0.1f)
        {
            girlfriend.transform.localPosition = Vector2.MoveTowards(girlfriend.transform.localPosition, TargetPosition, girlfriend.JumpSpeed * Time.deltaTime);
        }        
        else
        {
            girlfriend.transform.localPosition = TargetPosition;
            stateMachine.ChangeState(girlfriend.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
