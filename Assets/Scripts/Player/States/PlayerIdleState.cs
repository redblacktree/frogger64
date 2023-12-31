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
    }
    
    public override void Update() 
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();        
    }
}
