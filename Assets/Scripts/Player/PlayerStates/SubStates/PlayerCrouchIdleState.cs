using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityZero();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isExitingState)
            return;


        if(xInput !=0 )
            stateMachine.ChangeState(player.CrouchMoveState);
        else if(yInput != -1 && !cantStand)
            stateMachine.ChangeState(player.IdleState);
        else
        {
            core.Movement.SetVelocityX(0);
        }
    }
}
