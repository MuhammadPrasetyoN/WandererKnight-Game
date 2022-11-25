using System.Collections.Generic;
using UnityEngine;
 
public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
 
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;

    void Start()
    {
        canDealDamage = false;
    }
 
    void FixedUpdate()
    {
        if (canDealDamage)
        {
            RaycastHit hit;
 
            int layerMask = 1 << 9;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                // Debug.Log("MANTAF ");
                if (hit.transform.TryGetComponent(out Enemy enemy)){
                    Debug.Log(hit.collider.gameObject.name);
                    enemy.TakeDamage(weaponDamage);
                    enemy.HitVFX(hit.point);
                }
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