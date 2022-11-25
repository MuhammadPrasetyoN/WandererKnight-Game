using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;

    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;

    public bool isHoldingWeapon;
    private Animator animator;
    private PlayerCombat playerCombat;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        isHoldingWeapon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            if(isHoldingWeapon){
                if(!playerCombat.isAttacking){
                    animator.SetTrigger("sheathWeapon");
                }
            } else {    
                animator.SetTrigger("drawWeapon");
            }
        }
    }

    public void DrawWeapon(){
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        isHoldingWeapon = true;
        animator.SetBool("isCombat", isHoldingWeapon);
        Destroy(currentWeaponInSheath);
    }

    public void SheathWeapon(){
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        isHoldingWeapon = false;
        animator.SetBool("isCombat", isHoldingWeapon);
        // animator.SetTrigger("sheathWeapon");
        Destroy(currentWeaponInHand);
    }
}
