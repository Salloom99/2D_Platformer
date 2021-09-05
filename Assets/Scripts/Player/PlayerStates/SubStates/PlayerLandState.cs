using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isExitingState)
            return;
        if(xInput !=0 )
            stateMachine.ChangeState(player.MoveState);
        else if(isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
