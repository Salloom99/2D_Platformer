using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    
    public PlayerWallSlideState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isExitingState)
            return;
            
        core.Movement.SetVelocityY(-playerData.wallSlideVelocity);

        if(grabInput && yInput == 0)
            stateMachine.ChangeState(player.WallGrabState);

        
    }
}
