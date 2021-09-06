using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyIdleState : EnemeyState
{

    private float idleTime;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;



    public EnemeyIdleState(Enemey enemey, string animBoolName) : base(enemey, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemey.Core.Movement.SetVelocityX(0f);

        SetRandomIdleTime();
        isIdleTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();

        if(flipAfterIdle)
            enemey.Core.Movement.CheckIfShouldFlip(-enemey.Core.Movement.FacingDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isIdleTimeOver = Time.time >= startTime + idleTime;

        if(isIdleTimeOver)
            statemachine.ChangeState(enemey.MoveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(EnemyData.minIdleTime,EnemyData.maxIdleTime);
    }

}
