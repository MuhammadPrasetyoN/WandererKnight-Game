using System.Collections.Generic;
using UnityEngine;
 
public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
 
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    [SerializeField] private LayerMask _layerMask;

    void Start()
    {
        canDealDamage = false;
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canDealDamage)
        {
            
            RaycastHit raycastHit;
 
            int layerMask = 1 << 6;
            if (Physics.Raycast(transform.position, -transform.up, out raycastHit, weaponLength, layerMask))
            {
                    Debug.Log("YES");
                    Debug.Log("enemy has dealt damage");
                    // health.TakeDamage(weaponDamage);
                    // health.HitVFX(raycastHit.point);
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}