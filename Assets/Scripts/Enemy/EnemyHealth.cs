using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] float maxHealth = 50;
    [SerializeField] float health = 50;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;
    [SerializeField] Slider healthBar;
    AudioSource takeDamageSfx;

    private EnemyUIHealth enemyUIHealth;

    Animator animator;

    private void Awake()
    {
        enemyUIHealth = GetComponentInChildren<EnemyUIHealth>();
        GameObject hitSound = GameObject.FindWithTag("EnemyTakeDamageSfx");
        takeDamageSfx = hitSound.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar != null)
        {
            healthBar.value = health / maxHealth * 100;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
        takeDamageSfx.Play();
        if (health <= 0)
        {
            Die();
        }
    }

    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
        enemyUIHealth.SetUp();
        StartCoroutine("CloseHealthUI");
    }

    void Die()
    {
        Destroy(Instantiate(ragdoll, transform.position, transform.rotation), 3);
        Destroy(this.gameObject);

        QuestTarget questTarget = GetComponent<QuestTarget>();
        if (questTarget != null)
        {
            questTarget.QuestProgress();
        }
    }

    IEnumerator CloseHealthUI()
    {
        yield return new WaitForSeconds(3);
        enemyUIHealth.Close();
    }
}
