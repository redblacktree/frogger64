using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() 
    {
        base.Enter();

        player.rb.velocity = Vector2.zero;
    }
    
    public override void Update() 
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void AnimationFinished()
    {
        base.AnimationFinished();

        GameManager.Instance.RespawnPlayer();
    }
}
