using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerGroundedState
{   


    public PlayerSlideState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

     public override void Enter()
    {
        base.Enter();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
        player.StateMachine.SetSlideExitTime();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isExitingState)
            return;

        bool stopSliding = Time.time >= startTime + playerData.slideTime;

        if(yInput ==-1 && !stopSliding)
            core.Movement.SetVelocityX(playerData.slideVelocity * core.Movement.FacingDirection);
        else if(cantStand)
            stateMachine.ChangeState(player.CrouchIdleState);
        else if(cantStand && xInput !=0)
            stateMachine.ChangeState(player.CrouchMoveState);
        else if(xInput == 0)
            stateMachine.ChangeState(player.IdleState);
        else
            stateMachine.ChangeState(player.MoveState);
    }


}
