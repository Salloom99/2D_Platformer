using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyState
{
    protected Enemey enemey;
    protected EnemeyStatemachine statemachine;
    protected EnemyData EnemyData;

    private string animBoolName;

    protected float startTime;

    public EnemeyState(Enemey enemey,string animBoolName)
    {
        this.enemey = enemey;
        this.statemachine = enemey.StateMachine;
        this.EnemyData = enemey.EnemyData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        enemey.Anim.SetBool(animBoolName,true);
    }

    public virtual void Exit()
    {
        enemey.Anim.SetBool(animBoolName,false);
    }

    public virtual void LogicUpdate(){}

    public virtual void PhysicsUpdate(){}
}
