using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{

    public Rigidbody2D Rb  {get; private set;}

    public int FacingDirection { get; private set; }

    public Vector2 CurrentVelocity {get; private set;}
    private Vector2 workspace;
    private Vector2 VelZero = Vector2.zero;


    [SerializeField] private float movementSmooth = 0.015f;

    protected override void Awake()
    {
        base.Awake();
        FacingDirection =1;
        Rb = GetComponentInParent<Rigidbody2D>();
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rb.velocity;
    }

      #region Set Functions

    public void SetVelocityZero()
    {
        Rb.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction,angle.y * velocity);
        Rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityX(float x)
    {
        workspace.Set(x,CurrentVelocity.y);
        //Rb.velocity = workspace;
        Rb.velocity = Vector2.SmoothDamp(Rb.velocity,workspace,ref VelZero,movementSmooth);
        CurrentVelocity = Rb.velocity;
    }

    public void SetVelocityY(float y)
    {
        workspace.Set(CurrentVelocity.x,y);
        //Rb.velocity = workspace;
        Rb.velocity = Vector2.SmoothDamp(Rb.velocity,workspace,ref VelZero,0.002f);
        CurrentVelocity = Rb.velocity;
    }

    private void Flip()
    {
        FacingDirection *= -1;
        Rb.transform.Rotate(0f,180f,0f);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput !=0 && xInput != FacingDirection)
            Flip();
    }

    #endregion

}
