using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;

    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;
    private PlayerInputActions playerControls;
    private InputAction weaponAction;

    public bool isHoldingWeapon;
    private Animator animator;
    private PlayerCombat playerCombat;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        isHoldingWeapon = false;
    }

    private void OnEnable()
    {
        weaponAction = playerControls.Player.Weapon;
        weaponAction.Enable();

        weaponAction.performed += WeaponAction;
    }

    private void OnDisable()
    {
        weaponAction.performed -= WeaponAction;
        weaponAction.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     if (isHoldingWeapon)
        //     {
        //         if (!playerCombat.isAttacking)
        //         {
        //             animator.SetTrigger("sheathWeapon");
        //         }
        //     }
        //     else
        //     {
        //         animator.SetTrigger("drawWeapon");
        //     }
        // }
    }

    public void DrawWeapon()
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        isHoldingWeapon = true;
        animator.SetBool("isCombat", isHoldingWeapon);
        Destroy(currentWeaponInSheath);
    }

    public void SheathWeapon()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        isHoldingWeapon = false;
        animator.SetBool("isCombat", isHoldingWeapon);
        // animator.SetTrigger("sheathWeapon");
        Destroy(currentWeaponInHand);
    }

    private void WeaponAction(InputAction.CallbackContext context)
    {
        if (isHoldingWeapon)
        {
            if (!playerCombat.isAttacking)
            {
                animator.SetTrigger("sheathWeapon");
            }
        }
        else
        {
            animator.SetTrigger("drawWeapon");
        }
    }
}
