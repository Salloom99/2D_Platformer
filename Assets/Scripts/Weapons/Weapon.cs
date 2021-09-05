using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;

    private Animator baseAnimator;
    private Animator weaponAnimator;

    protected PlayerAttackState state;

    protected int attackCounter;
    protected bool inAir;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon(float time,bool isGrounded)
    {   
        gameObject.SetActive(true);

        inAir =!isGrounded;
        
        ResetCounterIfNeeded(time);

        baseAnimator.SetBool("attack",true);
        weaponAnimator.SetBool("attack",true);

        baseAnimator.SetBool("inAir",inAir);
        weaponAnimator.SetBool("inAir",inAir);

        baseAnimator.SetInteger("attackCounter",attackCounter);
        weaponAnimator.SetInteger("attackCounter",attackCounter);
    }

    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack",false);
        weaponAnimator.SetBool("attack",false);

        attackCounter++;

        gameObject.SetActive(false);
    }

    public void ResetCounterIfNeeded(float time)
    {

        bool timeOut = Time.time >= time + weaponData.comboTime;

        if( attackCounter >=weaponData.amountOfAttacks || timeOut)
            attackCounter = 0;
    }

    #region Animation Triggers

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter],weaponData.carrySpeed[attackCounter]); 
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f,0f); 
    }

    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {

    }

    public virtual void AnimationSetInAirTrigger()
    {
        inAir = state.GetInAir();

        baseAnimator.SetBool("inAir",inAir);
        weaponAnimator.SetBool("inAir",inAir);
    }

    public void InitializeWeapon(PlayerAttackState state) => this.state = state;

    #endregion
}
