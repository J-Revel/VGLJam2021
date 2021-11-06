using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player, Enemy, Neutral,
}

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 1;
    public float recoil = 10;
    public TeamDataHolder teamDataHolder;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = speed * transform.right;
        teamDataHolder = GetComponent<TeamDataHolder>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Health health = collider.GetComponent<Health>();
        TeamDataHolder otherTeamDataHolder = collider.GetComponent<TeamDataHolder>();
        Rigidbody2D otherRigidbody = collider.GetComponentInParent<Rigidbody2D>();
        if(otherRigidbody != null)
        {
            otherRigidbody.AddForce(recoil * transform.forward, ForceMode2D.Impulse);
        }
        if(otherTeamDataHolder == null)
        {
            Destroy(gameObject);
        }
        else if(otherTeamDataHolder.team != teamDataHolder.team)
        {
            if(health != null)
            {
                health.Damage(damage);
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
