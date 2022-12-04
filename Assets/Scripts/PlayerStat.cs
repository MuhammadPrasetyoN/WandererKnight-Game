using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance;
    public HealthBar healthBar;

    [SerializeField] float maxHealth = 1000;
    [SerializeField] float health;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;

    Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        // SetHealth();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
        SetHealth();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
        GameStateController.Instance.GameOver();
    }

    public void SetHealth()
    {
        healthBar.SetHealth(health, maxHealth);
    }

    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }

    public void ConsumePotion(int value)
    {
        health += value;
        SetHealth();
    }
}