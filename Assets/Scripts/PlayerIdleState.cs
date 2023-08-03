using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    
    public override void Enter() 
    {
        base.Enter();

        Debug.Log("Idle");
    }
    
    public override void Update() 
    {
        base.Update();

        Debug.Log("player.MoveInput: " + player.MoveInput);
        if (player.MoveInput != Vector2.zero)
        {
            Debug.Log(stateMachine);
            Debug.Log(this.player.MoveState);
            stateMachine.ChangeState(this.player.MoveState);
        }
    }

    public override void Exit()
    {
        base.Exit();        
    }
}
