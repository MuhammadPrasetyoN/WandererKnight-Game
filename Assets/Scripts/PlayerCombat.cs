using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private float attackGracePeriod = 1f;
    
    private Animator animator;
    private EquipmentSystem equipment;

    public bool isAttacking;
    private float? lastAttackTime;
    private float attackCombo;

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
            if(Input.GetMouseButtonDown(0)){
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
            
            if(Time.time - lastAttackTime <= attackGracePeriod){
                if(attackCombo < 3){
                    attackCombo++;
                } else {
                    attackCombo = 1;
                }
            } else {
                attackCombo = 1;
                isAttacking = false;
                lastAttackTime = null;
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
