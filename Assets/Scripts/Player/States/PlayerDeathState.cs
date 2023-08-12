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

        player.transform.rotation = Quaternion.identity;
        player.rb.velocity = Vector2.zero;
        player.col.enabled = false;
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

        player.Destroy();
        GameManager.Instance.PlayerDied();
    }
}
