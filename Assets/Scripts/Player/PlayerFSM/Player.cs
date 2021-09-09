using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    #region State Variable
    public PlayerStatemachine StateMachine {get; private set;}

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerSlideState SlideState {get; private set;}
    public PlayerAttackState PrimaryAttackState {get; private set;}
    public PlayerAttackState SecondaryAttackState {get; private set;}

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public BoxCollider2D BoxCollider {get; private set;}
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerInventory Inventory {get; private set;}

    #endregion

    #region Other Variables
    
    private Vector2 workspace;

    [SerializeField] private TextMesh stateText;

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStatemachine();

        IdleState = new PlayerIdleState(this,playerData,"idle");
        MoveState = new PlayerMoveState(this,playerData,"move");
        JumpState = new PlayerJumpState(this, playerData, "inAir");
        InAirState = new PlayerInAirState(this, playerData, "inAir");
        LandState = new PlayerLandState(this, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, playerData, "ledgeClimbState");
        CrouchIdleState = new PlayerCrouchIdleState(this, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, playerData, "crouchMove");
        SlideState = new PlayerSlideState(this,playerData,"slide");
        PrimaryAttackState = new PlayerAttackState(this,playerData,"attack");
        SecondaryAttackState = new PlayerAttackState(this,playerData,"attack");

    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Inventory = GetComponent<PlayerInventory>();

        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);
        StateMachine.Initialize(IdleState);

        StateMachine.SlideExitTime = -playerData.backToSlideTime;
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();

        stateText.text = StateMachine.CurrentState.AnimBoolName;
        stateText.transform.rotation = Quaternion.identity;

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();

    }
    #endregion

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = BoxCollider.offset;
        workspace.Set(BoxCollider.size.x,height);
        center.y += (height -BoxCollider.size.y) /2;
        BoxCollider.size = workspace;
        BoxCollider.offset = center;
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    public  void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


    
    #endregion
}
