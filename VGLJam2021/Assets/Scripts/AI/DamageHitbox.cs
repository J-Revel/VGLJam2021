using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if(player != null)
        {
            player.GetComponent<Health>().Damage(1, collision.GetContact(0).normal);
        }
    }

    void Update()
    {
        
    }
}
