using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private float attackGracePeriod = 1.5f;
    
    [SerializeField]
    private float attackCD = 1f;
    
    private Animator animator;
    private EquipmentSystem equipment;

    public bool isAttacking;
    private float? lastAttackTime;
    private float attackCombo;
    private float nextAttack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        equipment = GetComponent<EquipmentSystem>();
        isAttacking = false;
        attackCombo = 1;
    }

    // Update is called once per frame
    void Update()
    {   
        if(equipment.isHoldingWeapon){ 
            if(Input.GetButtonDown("Fire1")){
                Attack();

                if(Time.time - lastAttackTime <= attackGracePeriod){
                    if(attackCombo < 3){
                        attackCombo++;
                    } else {
                        attackCombo = 1;
                    }
                }
            } 

            if(Time.time - lastAttackTime > attackGracePeriod){
                attackCombo = 1;
                isAttacking = false;
                lastAttackTime = null;
            }
        }
    }

    public void Attack(){
        if(Time.time > nextAttack){
            nextAttack = Time.time + attackCD;
            lastAttackTime = Time.time;
            isAttacking = true;
            
            if(attackCombo == 1){
                animator.SetTrigger("doAttack"); 
            } else if(attackCombo == 2){
                animator.SetTrigger("doAttack2"); 
            } else if(attackCombo == 3){
                animator.SetTrigger("doAttack3"); 
            }
        }
    }


    public void StartDealDamage()
    {
        GetComponentInChildren<DamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}
