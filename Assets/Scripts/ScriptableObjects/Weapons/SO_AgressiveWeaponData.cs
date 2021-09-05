using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/Aggressive Weapon")]
public class SO_AgressiveWeaponData : SO_WeaponData 
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;

    public WeaponAttackDetails[] AttackDetails {get => attackDetails; private set => attackDetails = value;} 

    private void OnEnable() {
        amountOfAttacks = attackDetails.Length;
        movementSpeed = new float[amountOfAttacks];
        carrySpeed = new float[amountOfAttacks];

        for (int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
            carrySpeed[i] = attackDetails[i].carrySpeed;
        }
    }
}