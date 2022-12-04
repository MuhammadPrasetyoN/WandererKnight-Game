using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackGracePeriod = 1.5f;
    [SerializeField] private float attackCD = 1f;
    [SerializeField] AudioSource swordSwingSfx;

    private PlayerInputActions playerControls;
    private Animator animator;
    private EquipmentSystem equipment;
    private InputAction attack;

    public bool isAttacking;
    private float? lastAttackTime;
    private float attackCombo;
    private float nextAttack;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    private void OnEnable()
    {
        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += Attack;
    }

    private void OnDisable()
    {
        attack.performed -= Attack;
        attack.Disable();
    }

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
        if (equipment.isHoldingWeapon)
        {
            if (Time.time - lastAttackTime > attackGracePeriod)
            {
                attackCombo = 1;
                isAttacking = false;
                lastAttackTime = null;
            }
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (equipment.isHoldingWeapon)
        {
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackCD;
                lastAttackTime = Time.time;
                isAttacking = true;


                if (attackCombo == 1)
                {
                    animator.SetTrigger("doAttack");
                }
                else if (attackCombo == 2)
                {
                    animator.SetTrigger("doAttack2");
                }
                else if (attackCombo == 3)
                {
                    animator.SetTrigger("doAttack3");
                }
            }

            if (Time.time - lastAttackTime <= attackGracePeriod)
            {
                if (attackCombo < 3)
                {
                    attackCombo++;
                }
                else
                {
                    attackCombo = 1;
                }
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

    public void SwingSound()
    {
        swordSwingSfx.Play();
    }
}
