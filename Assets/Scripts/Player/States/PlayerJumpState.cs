using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public Vector2 TargetPosition;
    public Vector2 Direction;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {        
        base.Enter();

        player.Jumping = true;
        TargetPosition = (Vector2)player.transform.position + Direction;
        player.Flip(Direction);
    }

    public override void Exit()
    {
        base.Exit();

        player.Jumping = false;
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(player.transform.position, TargetPosition) > 0.01f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, TargetPosition, player.jumpSpeed * Time.deltaTime);
        }        
        else
        {
            player.transform.position = TargetPosition;
            if (player.HomeSafe)
            {
                stateMachine.ChangeState(player.HomeState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
