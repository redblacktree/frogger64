using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public Vector2 TargetWorldPosition;
    public Vector2 TargetGridPosition;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {        
        base.Enter();

        Flip(TargetGridPosition - player.GridPosition);
        if (GameManager.Instance.map.IsDeadly(TargetGridPosition))
        {
            player.GonnaDie = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(player.transform.position, TargetWorldPosition) > 0.01f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, TargetWorldPosition, player.moveSpeed * Time.deltaTime);
        }        
        else
        {
            player.transform.position = TargetWorldPosition;
            stateMachine.ChangeState(player.IdleState);
        }

        Debug.Log(player.GonnaDie);

        if (player.GonnaDie && Vector2.Distance(player.transform.position, TargetWorldPosition) < GameManager.Instance.MovementDeathDistance)
        {
            stateMachine.ChangeState(player.DeathState);
        }
    }

    private void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        else if (direction.x < 0)
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (direction.y > 0)
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (direction.y < 0)
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }
}
