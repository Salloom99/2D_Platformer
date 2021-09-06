using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyMoveState : EnemeyState
{

    private bool isTouchingWall;
    private bool isTouchingLedge;

    public EnemeyMoveState(Enemey enemey, string animBoolName) : base(enemey, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemey.Core.Movement.SetVelocityX(EnemyData.movementSpeed * enemey.Core.Movement.FacingDirection);

        isTouchingWall = enemey.Core.CollisionSenses.WallFront;
        isTouchingLedge = enemey.Core.CollisionSenses.DownLedge;
    }

    public override void Exit()
    {
        base.Exit();

        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isTouchingWall = enemey.Core.CollisionSenses.WallFront;
        isTouchingLedge = enemey.Core.CollisionSenses.DownLedge;

        Debug.Log("ledge "+isTouchingLedge);
        Debug.Log("wall "+isTouchingWall);

        if(!isTouchingLedge || isTouchingWall)
            statemachine.ChangeState(enemey.IdleState);
        else
        {
            enemey.Core.Movement.SetVelocityX(EnemyData.movementSpeed * enemey.Core.Movement.FacingDirection);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
