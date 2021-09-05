using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{

    int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        core.Movement.SetVelocityY(playerData.jumpVelocity);
        isAblitiyDone = true;
        UseJump();
        player.InAirState.SetIsJumping();
    }

    public bool CanJump() => amountOfJumpsLeft > 0;

    public void Resetjumps() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void UseJump() => amountOfJumpsLeft--;
}
