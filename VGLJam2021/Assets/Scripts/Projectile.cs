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
    public Transform impactFx;
    public LayerMask impactLayer;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = speed * transform.right;
        teamDataHolder = GetComponent<TeamDataHolder>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.collider.GetComponent<Health>();
        TeamDataHolder otherTeamDataHolder = collision.collider.GetComponent<TeamDataHolder>();
        Rigidbody2D otherRigidbody = collision.collider.GetComponentInParent<Rigidbody2D>();
        
        if(otherTeamDataHolder == null)
        {
            Instantiate(impactFx, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.right, collision.contacts[0].normal), Vector3.forward));
            Destroy(gameObject);
        }
        else if(otherTeamDataHolder.team != teamDataHolder.team)
        {
            if(health != null)
            {
                health.Damage(damage, transform.right);
            }
            if(otherRigidbody != null)
            {
                otherRigidbody.AddForce(recoil * transform.right, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
