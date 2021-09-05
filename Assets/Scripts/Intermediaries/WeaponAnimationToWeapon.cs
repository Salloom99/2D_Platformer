using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon weapon;

    public void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }

    private void AnimationFinishTrigger() => weapon.AnimationFinishTrigger();

    public void AnimationStartMovementTrigger() =>weapon.AnimationStartMovementTrigger();

    public void AnimationStopMovementTrigger() =>weapon.AnimationStopMovementTrigger();

    public virtual void AnimationTurnOffFlipTrigger() =>weapon.AnimationTurnOffFlipTrigger();

    public virtual void AnimationTurnOnFlipTrigger() =>weapon.AnimationTurnOnFlipTrigger();

    public virtual void AnimationTurnSetInAirTrigger() =>weapon.AnimationSetInAirTrigger();

    public virtual void AnimationActionTrigger()=> weapon.AnimationActionTrigger();
}
