using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isExitingState)
            return;
        core.Movement.SetVelocityY(playerData.wallClimbVelocity);

        if(yInput !=1)
            stateMachine.ChangeState(player.WallGrabState);
    }
}
