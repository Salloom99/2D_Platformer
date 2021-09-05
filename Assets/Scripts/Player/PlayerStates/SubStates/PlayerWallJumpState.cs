using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        core.Movement.SetVelocity(playerData.wallJumpVelocity,playerData.walljumpAngle,wallJumpDirection);
        core.Movement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.UseJump();

        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity",player.Core.Movement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity",Mathf.Abs(player.Core.Movement.CurrentVelocity.x));

        if(Time.time >= startTime + playerData.wallJumpTime)
            isAblitiyDone=true;
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if(isTouchingWall)
            wallJumpDirection = -core.Movement.FacingDirection;
        else
            wallJumpDirection = core.Movement.FacingDirection;
    }
}
