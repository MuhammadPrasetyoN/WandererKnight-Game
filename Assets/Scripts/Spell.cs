using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    [SerializeField] private float lifeDuration = 3.5f;
    [SerializeField] private float spellDamage;
    [SerializeField] private GameObject hitVFX;

    private void Awake()
    {
        // hitSound = GameObject.FindWithTag("HitSfx");
        // hitSfx = hitSound.GetComponent<AudioSource>();
        Destroy(gameObject, lifeDuration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Destroy(Instantiate(hitVFX, pos, rot), 1.5f);

        if (collision.gameObject.layer == 6)
        {
            Debug.Log("KENA");
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(spellDamage);
            collision.gameObject.GetComponent<PlayerHealth>().HitVFX(contact.point);
        }

        Destroy(gameObject);
    }
}
