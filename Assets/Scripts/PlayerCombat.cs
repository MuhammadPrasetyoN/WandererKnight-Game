using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private EquipmentSystem equipment;

    public bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        equipment = GetComponent<EquipmentSystem>();
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if(equipment.isHoldingWeapon){ 
            if(Input.GetMouseButtonDown(0)){
                animator.SetTrigger("doAttack");
                isAttacking = true;
            } else {
                isAttacking = false;
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
