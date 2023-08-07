using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHomeState : PlayerState
{
    public PlayerHomeState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter() 
    {
        base.Enter();

        // we don't want input to move this player entity anymore
        player.GetComponent<PlayerInput>().enabled = false;

        // center the player on the home square
        player.transform.position = new Vector2(Mathf.RoundToInt(player.transform.position.x), 3.5f);
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
