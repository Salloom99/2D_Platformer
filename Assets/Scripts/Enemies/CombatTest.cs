using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTest : MonoBehaviour,IDamageable
{

    [SerializeField] private GameObject hitParticles;

    private Animator anim;

    private float wiggleTime = 2f;
    private float startTimer;

    public void Damage(float Damage)
    {
        Debug.Log(Damage + "amount of damage taken");

        Instantiate(hitParticles,transform.position,Quaternion.Euler(0f,0f,Random.Range(0f,360f)));
        anim.SetBool("damage",true);
        startTimer = Time.time;;
        //Destroy(gameObject);
    }

    private void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    private void Update() {

        if( Time.time >= startTimer + wiggleTime)
        {
            anim.SetBool("damage",false);
            Debug.Log("stopped");
        }

    }

    private void enoughHitting()
    {
        anim.SetBool("damage",false);
    }
}
