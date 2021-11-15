using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player, Enemy, Neutral,
}

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 1;
    public float recoil = 10;
    public TeamDataHolder teamDataHolder;
    public Transform impactFx;
    public LayerMask impactLayer;
    public float minVelocityRatio = 0.3f;
    public Transform bouncePrefab;
    public LayerMask collisionLayer;
    
    void Start()
    {
        teamDataHolder = GetComponent<TeamDataHolder>();
    }



    private void OnCollisionWith(Collider2D otherCollider, Vector2 contactPoint, Vector2 normal)
    {
        Health health = otherCollider.GetComponent<Health>();
        TeamDataHolder otherTeamDataHolder = otherCollider.GetComponent<TeamDataHolder>();
        Rigidbody2D otherRigidbody = otherCollider.GetComponentInParent<Rigidbody2D>();
        Bumper bumper = otherCollider.GetComponent<Bumper>();
        
        if(otherTeamDataHolder == null)
        {
            transform.position = contactPoint + normal * 0.01f;
            Quaternion normalAngle = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.right, normal), Vector3.forward);
            Instantiate(impactFx, contactPoint, normalAngle, LevelContainer.instance.transform);
            
            if(bumper != null)
            {
                transform.rotation = normalAngle * Quaternion.AngleAxis(Vector2.SignedAngle(-transform.right, normal), Vector3.forward);
            }
            else
            {
                Destroy(gameObject);
            }
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
        float distance = speed * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance, collisionLayer);
        if(hit.collider != null)
        {
            distance = hit.distance;
            
            OnCollisionWith(hit.collider, hit.point, hit.normal);
        }
        else
        {
            transform.position += transform.right * distance;
        }
        // if(rigidbody.velocity.sqrMagnitude < minVelocityRatio * minVelocityRatio * speed * speed)
        // {
        //     Destroy(gameObject);
        // }
    }
}
