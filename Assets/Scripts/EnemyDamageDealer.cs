using System.Collections.Generic;
using UnityEngine;
 
public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
 
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    [SerializeField] private LayerMask _layerMask;

    private Vector3 collision = Vector3.zero;

    void Start()
    {
        canDealDamage = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (canDealDamage)
        {
            
            RaycastHit raycastHit;
 
            // int layerMask = 1 << 6;
            if (Physics.Raycast(transform.position, transform.up, out raycastHit, weaponLength, _layerMask))
            {
                if (raycastHit.transform.TryGetComponent(out HealthSystem health)){
                    Debug.Log("enemy has dealt damage");
                    health.TakeDamage(weaponDamage);
                    health.HitVFX(raycastHit.point);

                    collision = raycastHit.point;
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
        Gizmos.DrawLine(transform.position, transform.position + transform.up * weaponLength);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}