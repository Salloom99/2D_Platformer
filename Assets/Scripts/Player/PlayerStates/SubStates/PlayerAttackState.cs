using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{

    private Weapon weapon;

    private int xInput;

    private float xVelocityToSet;
    private float yVelocityToSet;
    private bool setVelocity;
    private bool shouldFlip;
    private bool isGrounded;

    private float exitTime;

    public PlayerAttackState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();
        setVelocity = false;
        weapon.EnterWeapon(exitTime,isGrounded);
    }

    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
        exitTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        if(shouldFlip)
            core.Movement.CheckIfShouldFlip(xInput);

        if (setVelocity && isGrounded)
            core.Movement.SetVelocityX(xVelocityToSet *core.Movement.FacingDirection);

        if(!isGrounded)
            core.Movement.SetVelocityY(yVelocityToSet);
    }

    public void SetWeapon( Weapon weapon)
    {
        weapon.InitializeWeapon(this);
        this.weapon = weapon;

    }

    public void SetPlayerVelocity(float xVel,float yVel)
    {
        core.Movement.SetVelocityX(xVel*core.Movement.FacingDirection);
        xVelocityToSet = xVel;

         yVelocityToSet = yVel;
        core.Movement.SetVelocityY(yVel);


        setVelocity = true;

       
    }

    public void SetFlipCheck(bool value)
    {
        shouldFlip = value;
    }

    public bool GetInAir() => !isGrounded;

    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAblitiyDone = true;
    }

    #endregion
}
