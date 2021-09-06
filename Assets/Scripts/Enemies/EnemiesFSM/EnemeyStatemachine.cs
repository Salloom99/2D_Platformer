using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyStatemachine 
{
    public EnemeyState CurrentState { get; private set; }

    public void Initialize(EnemeyState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(EnemeyState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
