using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public Transform GroundCheck {get => groundCheck; set => groundCheck = value;}
    public Transform CeilingCheck{get => ceilingCheck; set => ceilingCheck = value;}
    public Transform WallCheck{get => wallCheck; set => wallCheck = value;}
    public Transform UpperLedgeCheck{get => upperLedgeCheck; set => upperLedgeCheck = value;}
    public Transform DownLedgeCheck{get => upperLedgeCheck; set => upperLedgeCheck = value;}

    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public float LedgeCheckDistance { get => ledgeCheckDistance; set => ledgeCheckDistance = value; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform upperLedgeCheck;
    [SerializeField] private Transform downLedgeCheck;
    

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float ledgeCheckDistance;
    

    #region Check Properties

    public bool Ground
    {
        get => Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,whatIsGround);
    }

     public bool Ceiling
    {
        get => Physics2D.OverlapCircle(ceilingCheck.position,groundCheckRadius,whatIsGround);
    }

    public bool WallFront
    {
        get => Physics2D.Raycast(wallCheck.position,Vector2.right * core.Movement.FacingDirection,wallCheckDistance,whatIsGround);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(wallCheck.position,Vector2.right * -core.Movement.FacingDirection,wallCheckDistance,whatIsGround);
    }

    public bool UpperLedge
    {
        get => Physics2D.Raycast(upperLedgeCheck.position,Vector2.right * core.Movement.FacingDirection,wallCheckDistance,whatIsGround);
    }
    
    public bool DownLedge
    {
        get => Physics2D.Raycast(downLedgeCheck.position,Vector2.down,ledgeCheckDistance,whatIsGround);
    }

    #endregion

}
