using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPostion;

    public PlayerWallGrabState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        holdPostion = player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isExitingState)
            return;
        if(yInput >0)
            stateMachine.ChangeState(player.WallClimbState);
        else if (yInput<0 || !grabInput)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        core.Movement.SetVelocityX(0f);
        core.Movement.SetVelocityY(0f);

        player.transform.position = holdPostion;
    }

}
