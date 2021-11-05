using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 1;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = speed * transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Health health = collider.GetComponent<Health>();
        if(health != null)
        {
            health.Damage(damage);
        Destroy(gameObject);
        }
        
    }

    void Update()
    {
        
    }
}
