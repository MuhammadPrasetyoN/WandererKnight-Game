using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 4f;
    [SerializeField] float aggroRange = 6f;
    [SerializeField] float enemyAreaRange = 8f;

    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;

    private Vector3 initialPosition;
    private float lastCheck = 0;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if (player == null)
        {
            return;
        }

        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                agent.SetDestination(transform.position);
                animator.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;

        if (Vector3.Distance(player.transform.position, transform.position) > attackRange)
        {
            if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
            {
                newDestinationCD = 0.5f;
                agent.SetDestination(player.transform.position);
            }
        }

        newDestinationCD -= Time.deltaTime;

        if (Time.time - lastCheck > 3.0f)
        {
            BackToInitial();
            lastCheck = Time.time;
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            Vector3 targetPostition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPostition);
        }

    }

    private void BackToInitial()
    {
        // Debug.Log("Checking Position to back");
        if (Vector3.Distance(transform.position, initialPosition) > enemyAreaRange)
        {
            agent.SetDestination(initialPosition);

            // if(health < maxHealth){
            //     health = maxHealth;
            // }
        }
    }

    public void CastSpell()
    {
        GetComponentInChildren<WizardWeapon>().Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(initialPosition, enemyAreaRange);
    }
}
