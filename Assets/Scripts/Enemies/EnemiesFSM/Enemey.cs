using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemey : MonoBehaviour
{
    #region State Variable
    public EnemeyStatemachine StateMachine { get; private set; }

    public EnemeyIdleState IdleState { get; private set; }
    public EnemeyMoveState MoveState { get; private set; }

    [SerializeField] private EnemyData enemyData;
    public EnemyData EnemyData { get => enemyData; set => enemyData = value; }
    
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public BoxCollider2D BoxCollider {get; private set;}
    #endregion


    #region Other Variables
    private Vector2 workspace;
    #endregion

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new EnemeyStatemachine();

        IdleState = new EnemeyIdleState(this,"idle");
        MoveState = new EnemeyMoveState(this,"move");
    }
    
    public virtual void Start()
    {
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();

        StateMachine.Initialize(MoveState);
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();    
    }

    public virtual void OnDrawGizmos() {
        Gizmos.DrawLine(
            Core.CollisionSenses.WallCheck.position,
            Core.CollisionSenses.WallCheck.position + (Vector3)(Vector2.right *Core.Movement.FacingDirection *Core.CollisionSenses.WallCheckDistance ));
        Gizmos.DrawLine(
            Core.CollisionSenses.DownLedgeCheck.position,
            Core.CollisionSenses.DownLedgeCheck.position + (Vector3)(Vector2.down *Core.CollisionSenses.LedgeCheckDistance ));
            
    }
}
