using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public void OnAnimationFinished()
    {
        GameManager.Instance.Player.StateMachine.CurrentState.AnimationFinished();
    }
}
