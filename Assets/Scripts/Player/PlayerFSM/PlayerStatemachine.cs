using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatemachine
{
    public PlayerState CurrentState { get; private set; }

    public float SlideExitTime;

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public float GetSlideExitTime()
    {
        return SlideExitTime;
    }

    public void SetSlideExitTime()
    {
        SlideExitTime = Time.time;
    }
}
