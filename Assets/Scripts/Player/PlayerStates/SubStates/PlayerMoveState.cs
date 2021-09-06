using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private bool canSlide;

    public PlayerMoveState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        canSlide = true;
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.CheckIfShouldFlip(xInput);

        core.Movement.SetVelocityX(playerData.movementVelocity * xInput);

        if(isExitingState)
            return;

        if(xInput == 0)
            stateMachine.ChangeState(player.IdleState);
        else if(yInput ==-1 && canSlide)
            stateMachine.ChangeState(player.SlideState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
