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

        // Remove any girlfriends that might be attached, and score them
        var girlfriend = player.GetComponentInChildren<Girlfriend>();
        if (girlfriend != null)
        {
            GameManager.Instance.AddScore(GameManager.Instance.ScoreForSavingGirlfriend);
            GameObject.Destroy(girlfriend.gameObject);
        }

        // we don't want input to move this player entity anymore
        player.GetComponent<PlayerInput>().enabled = false;

        // center the player on the home square
        HomeSquare homeSquare = FindNearestHomeSquare(player.transform.position);
        player.transform.position = homeSquare.HomeLocation;
        homeSquare.Occupy();

        // score
        GameManager.Instance.AddScore(GameManager.Instance.ScorePerHomeSquare);
        int timeRemaining = (int)GameManager.Instance.TimeRemaining;
        GameManager.Instance.AddScore(GameManager.Instance.ScorePerSecondRemaining * timeRemaining);
        GameManager.Instance.DisplayMessage("TIME " + timeRemaining, 1f);
    }

    private HomeSquare FindNearestHomeSquare(Vector2 position)
    {
        HomeSquare nearest = GameManager.Instance.HomeSquares[0];
        float distance = Mathf.Infinity;

        foreach (HomeSquare homeSquare in GameManager.Instance.HomeSquares)
        {
            float newDistance = Vector2.Distance(position, homeSquare.HomeLocation);
            if (newDistance < distance)
            {
                distance = newDistance;
                nearest = homeSquare;
            }
        }

        return nearest;
    }
}
