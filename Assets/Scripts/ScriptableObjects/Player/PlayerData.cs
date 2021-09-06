using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 8f;
    public float movementSmooth = .015f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("InAir State")]
    public float coyoteTime =.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 14f;
    public float wallJumpTime = 0.3f;
    public Vector2 walljumpAngle = new Vector2(1,2.5f);

    [Header("Ledge Climb State")]
    public Vector2 startOffset = new Vector2(0.3f,0.5f);
    public Vector2 stopOffset = new Vector2(0.3f,0.7f);

    [Header("Crouch States")]
    public float crouchVelocity = 2f;
    public float crouchColliderHeight = 0.6f;
    public float standColliderHeight = 0.9f;

    [Header("Slide State")]
    public float slideVelocity = 10f;
    public float slideTime = 0.5f;
    public float backToSlideTime = 1f;

}
