using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardWeapon : MonoBehaviour
{

    [SerializeField] private Transform shotPoint;
    [SerializeField] private float shotForce = 1000f;
    [SerializeField] private Rigidbody spell;
    public void Attack()
    {
        Rigidbody shot = Instantiate(spell, shotPoint.position, shotPoint.rotation) as Rigidbody;
        shot.AddForce(shotPoint.forward * shotForce);
    }
}
