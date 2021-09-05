using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTest : MonoBehaviour,IDamageable
{

    [SerializeField] private GameObject hitParticles;

    private Animator anim;

    public void Damage(float Damage)
    {
        Debug.Log(Damage + "amount of damage taken");

        Instantiate(hitParticles,transform.position,Quaternion.Euler(0f,0f,Random.Range(0f,360f)));
        anim.SetTrigger("damage");

        Destroy(gameObject);
    }

    private void Awake() 
    {
        anim = GetComponent<Animator>();
    }
}
