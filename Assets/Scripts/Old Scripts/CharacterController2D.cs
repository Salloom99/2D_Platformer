using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    // Parameters
	float JumpForce = 15f;
    float KnockForce = 200f;
	float MovementSmoothing = .05f;
    float runSpeed = 10f;
    float wallSlidingSpeed = 1f;
	float horizontalMove = 0f;


    // Booleans
    [SerializeField]bool canSlide = true;
	[SerializeField]bool wannaJump = false;
	[SerializeField]bool wannaCrouch = false;
    // [SerializeField]bool wannaSlide = false;
    [SerializeField]bool doubleJump = true;
    [SerializeField]bool knocked = false;
    [SerializeField]bool relaxed = true;
    [SerializeField]bool touchingWall = false;
    [SerializeField]bool sliding = false;
    [SerializeField]bool slowing = false;
    [SerializeField]bool moving = false;
    [SerializeField]bool falling = false;
    // [SerializeField]bool jumping = false;
	[SerializeField]bool crouching = false;
    [SerializeField]bool attacking = false;
	[SerializeField]bool Grounded;
    [SerializeField]bool wasGrounded;
	[SerializeField]bool facingRight = true;

    // Checkers
	[SerializeField] LayerMask WhatIsGround;
    [SerializeField] LayerMask WhatIsDamagable;
	Vector3 GroundCheck;
	Vector3 CeilingCheck;
    Vector3 HitCircle;


    // Components
	Rigidbody2D rgBody;
    Animator animator;
    CapsuleCollider2D playerCollider;

	Vector3 m_Velocity = Vector3.zero;
    float attackTimer =0;
    float jumpTimer =0;
    float relaxTimer=0;
    float slideTimer=0;


	private void Awake()
	{
		rgBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        GroundCheck = transform.position + new Vector3(0,-0.55f,0);
        CeilingCheck = transform.position + new Vector3(0,0.37f,0);
	}

    void Update () {
        
        //Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        GroundCheck = transform.position + new Vector3(0,-0.55f,0);
        CeilingCheck = transform.position + new Vector3(0,0.37f,0);

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			wannaJump = true;
            animator.SetTrigger("jump");
            jumpTimer =20;
		}

        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("knock");
            if(!knocked)
                rgBody.AddForce(new Vector2((facingRight?-1:1)* KnockForce,KnockForce));
            knocked = !knocked;
        }

		if (Input.GetButtonDown("Crouch") )
			wannaCrouch = true;
		else if (Input.GetButtonUp("Crouch"))
			wannaCrouch = false;

        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("attack");
            attackTimer = 30;
        }

        if (Input.GetButtonDown("Walk"))
        {
			slowing = true;
            runSpeed = 10f;
        }
        else if (Input.GetButtonUp("Walk"))
        {
			slowing = false;
            runSpeed = 30f;
        }
	}

	private void FixedUpdate()
	{
        
        checkTimers();

        if(rgBody.velocity.y < 0)
            falling=true;
        
        if (horizontalMove !=0)
            moving = true;
        else
            moving= false;

        if(!moving)
            sliding = false;
		wasGrounded = Grounded;
		Grounded = false;

		isGrounded();
        checkWallTouching();

        if(touchingWall && falling && moving && !Grounded)
            if(rgBody.velocity.y < - wallSlidingSpeed)
                rgBody.velocity = new Vector2(rgBody.velocity.x,-wallSlidingSpeed);

        if(touchingWall && moving)
            doubleJump=true;
        if(!knocked)
            Move();

        setAnimatorParams();
		wannaJump = false;

        if(!sliding && attacking || slowing || crouching )
            runSpeed = 10f;
        else if (!attacking && !slowing && !crouching)
            runSpeed = 30f;

	}


	public void Move()
	{
        float move = horizontalMove * Time.fixedDeltaTime;
		if (!wannaCrouch && Grounded)
			if (Physics2D.OverlapCircle(CeilingCheck, .2f, WhatIsGround))
				wannaCrouch = true;

	
        if (wannaCrouch && Grounded)
        {
            if (!crouching)
            {
                crouching = true;
                sliding = false;
                playerCollider.offset = new Vector2(playerCollider.offset.x,-0.2f);
                playerCollider.size = new Vector2(playerCollider.size.x,0.7f);

                if(Grounded && moving && !sliding && canSlide)
                    Invoke("slide",.1f);

            }

            
        } else
        {
            if (crouching)
            {
                crouching = false;
                playerCollider.offset = new Vector2(playerCollider.offset.x,-0.1f);
                playerCollider.size = new Vector2(playerCollider.size.x,0.9f);
            }
            
        }

        Vector3 targetVelocity = new Vector2(move * 10f, rgBody.velocity.y);
        rgBody.velocity = Vector3.SmoothDamp(rgBody.velocity, targetVelocity, ref m_Velocity, MovementSmoothing);

        if(!attacking)
            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();

		if ((Grounded || doubleJump)&& wannaJump)
		{
            if(!moving)
                Invoke("jump",0.1f);
            else if (touchingWall && falling && moving && !Grounded)
                wallJump();
            else
                jump();
		}
	}
    private void jump()
    {
        Grounded = false;
        rgBody.velocity = new Vector2(rgBody.velocity.x,JumpForce);

        if(doubleJump)
            doubleJump =false;
    }

    private void wallJump()
    {
        rgBody.velocity = new Vector2((facingRight?-2:2)*JumpForce,JumpForce);
        doubleJump =false;
    }

    private void slide()
    {
        rgBody.AddForce(new Vector2((facingRight?1:-1)* 20,0),ForceMode2D.Impulse);
        sliding = true;
        canSlide = false;
        slideTimer = 60;
    }

    
    private void setAnimatorParams()
    {
        animator.SetBool("falling",falling);
        animator.SetBool("crouching",crouching);
        animator.SetBool("moving",moving);
        animator.SetBool("walk",slowing);
        animator.SetBool("grounded",Grounded);
        animator.SetBool("knocked",knocked);
        animator.SetBool("touching",touchingWall);
        animator.SetBool("sliding",sliding);

    }

	private void Flip()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
	}

    private void finished_attacking(){
        relaxTimer = 360;
        attacking = false;
    }

    private void start_attacking(){
        attacking = true;
        relaxed = false;
    }

    private void isGrounded()
    {
        
		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck, .2f, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
                falling=false;
				if (!wasGrounded)
					doubleJump =true;
			}
		}
    }

    private void checkWallTouching()
    {

        touchingWall = Physics2D.Raycast(transform.position,transform.right,0.4f,WhatIsGround);
        touchingWall |= Physics2D.Raycast(transform.position,transform.right,-0.4f,WhatIsGround);
    }


    private void checkDamaged()
    {
        
		Collider2D[] colliders = Physics2D.OverlapCircleAll(HitCircle, .2f, WhatIsDamagable);
		foreach (Collider2D collider in colliders)
		{
			collider.transform.parent.SendMessage("Damage",100);
		}
    }

    private void checkTimers()
    {
        if(attackTimer>0)
            attackTimer--;
        else
        {
            attackTimer =0;
            animator.ResetTrigger("attack");
        }

        if(jumpTimer>0)
            jumpTimer--;
        else
        {
            jumpTimer =0;
            animator.ResetTrigger("jump");
        }

        if(relaxTimer>0 &&!attacking)
            relaxTimer--;
        else if(!relaxed && !attacking)
        {
            relaxTimer =0;
            animator.SetTrigger("relax");
            relaxed = true;
        }

        if(slideTimer>0)
            slideTimer--;
        else
        {
            slideTimer=0;
            canSlide =true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,-0.55f,0),.2f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,0.37f,0),.2f);
        Gizmos.DrawLine(transform.position,transform.position+new Vector3(.2f,0,0));
    }

}