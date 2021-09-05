using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
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

        core.Movement.CheckIfShouldFlip(xInput);
        core.Movement.SetVelocityX(playerData.crouchVelocity*xInput);

        if(xInput ==0 )
            stateMachine.ChangeState(player.CrouchIdleState);
        else if(yInput !=-1 && !cantStand)
            stateMachine.ChangeState(player.MoveState);

    }
}
