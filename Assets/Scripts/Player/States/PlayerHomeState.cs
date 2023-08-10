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

        Debug.Log("PlayerHomeState: Enter()");
        // we don't want input to move this player entity anymore
        player.GetComponent<PlayerInput>().enabled = false;

        // center the player on the home square
        player.transform.position = FindNearestHomeSquare(player.transform.position);

        GameManager.Instance.AddScore(GameManager.Instance.ScorePerHomeSquare);
        int timeRemaining = (int)GameManager.Instance.TimeRemaining;
        GameManager.Instance.AddScore(GameManager.Instance.ScorePerSecondRemaining * timeRemaining);
        GameManager.Instance.DisplayMessage("TIME " + timeRemaining, 1f);
    }

    private Vector2 FindNearestHomeSquare(Vector2 position)
    {
        Vector2 nearest = Vector2.zero;
        float distance = Mathf.Infinity;

        foreach (Vector2 homeSquare in GameManager.Instance.HomeSquareLocations)
        {
            float newDistance = Vector2.Distance(position, homeSquare);
            if (newDistance < distance)
            {
                distance = newDistance;
                nearest = homeSquare;
            }
        }

        return nearest;
    }
}
