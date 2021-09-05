using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggressiveWeapon : Weapon
{

    protected SO_AgressiveWeaponData agressiveWeaponData;
    private List<IDamageable> detectedDamageables = new List<IDamageable>();

    protected override void Awake()
    {
        base.Awake();

        if(weaponData.GetType() == typeof(SO_AgressiveWeaponData))
            agressiveWeaponData = (SO_AgressiveWeaponData)weaponData;
        else
            Debug.LogError("Wrong Data for the weapon");
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = agressiveWeaponData.AttackDetails[attackCounter];

        foreach (IDamageable damagable in detectedDamageables.ToList())
        {
            damagable.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D col)
    {
        IDamageable damageable = col.GetComponent<IDamageable>();
        if(damageable != null)
            detectedDamageables.Add(damageable);
    }

    public void RemoveFromDetected(Collider2D col)
    {
        IDamageable damageable = col.GetComponent<IDamageable>();
        if(damageable != null)
            detectedDamageables.Remove(damageable);
    }

    


}
