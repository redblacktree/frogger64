using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Vector2 targetSquare;
    private Vector2 targetWorldPosition;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {        
        base.Enter();

        FindTargetSquare();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(player.transform.position, targetWorldPosition) > 0.01f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetWorldPosition, player.moveSpeed * Time.deltaTime);
        }
        else
        {
            player.transform.position = targetWorldPosition;
            stateMachine.ChangeState(player.IdleState);
        }
    }

    private void FindTargetSquare()
    {
        Vector2 targetSquare = Vector2.zero;

        if (player.MoveInput.x > 0)
        {
            targetSquare = Vector2.right;
        }
        else if (player.MoveInput.x < 0)
        {
            targetSquare = Vector2.left;
        }
        else if (player.MoveInput.y > 0)
        {
            targetSquare = Vector2.up;
        }
        else if (player.MoveInput.y < 0)
        {
            targetSquare = Vector2.down;
        }

        Vector2 playerGridPosition = player.GridPosition;
        targetSquare += playerGridPosition;
        targetWorldPosition = player.gameManager.map.GridCoordToWorldPoint(targetSquare);
    }
}
