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

        Debug.Log("Jump State Enter");

        TargetPosition = (Vector2)girlfriend.transform.localPosition + girlfriend.FacingDirection;
        Debug.Log("Target Position: " + TargetPosition);
        Debug.Log("Girlfriend Position: " + girlfriend.transform.localPosition);
    }
    
    public override void Update() 
    {
        base.Update();

        Debug.Log("Update");

        Debug.Log(Vector2.Distance(girlfriend.transform.localPosition, TargetPosition));
        if (Vector2.Distance(girlfriend.transform.localPosition, TargetPosition) > 0.01f)
        {
            Debug.Log("Move Towards Target Position");
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
