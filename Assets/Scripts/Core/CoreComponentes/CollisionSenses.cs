using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public Transform GroundCheck {get => groundCheck; set => groundCheck = value;}
    public Transform CeilingCheck{get => ceilingCheck; set => ceilingCheck = value;}
    public Transform WallCheck{get => wallCheck; set => wallCheck = value;}
    public Transform LedgeCheck{get => ledgeCheck; set => ledgeCheck = value;}

    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    

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

    public bool Ledge
    {
        get => Physics2D.Raycast(ledgeCheck.position,Vector2.right * core.Movement.FacingDirection,wallCheckDistance,whatIsGround);
    }
    


    #endregion

}
