using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlfriendRidingState : State
{
    private Girlfriend girlfriend;

    public GirlfriendRidingState(Girlfriend girlfreind, StateMachine stateMachine, string animBoolName) : base(girlfreind, stateMachine, animBoolName)
    {
        this.girlfriend = girlfreind;
    }

    public override void Enter() 
    {
        base.Enter();
    }
    
    public override void Update() 
    {
        base.Update();

        //girlfriend.transform.rotation = GameManager.Instance.Player.rotation;        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
