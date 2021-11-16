using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPush : MonoBehaviour
{
    public float range;
    public float maxPushForce;
    public float minPushForce;
    public Transform fx;
    
    public void Push()
    {
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(transform.position, range))
        {
            TeamDataHolder teamDataHolder = collider.GetComponentInParent<TeamDataHolder>();
            if(teamDataHolder != null && teamDataHolder.team == Team.Enemy)
            {
                Vector3 forceDirection = collider.transform.position - transform.position;
                collider.attachedRigidbody.AddForce(forceDirection.normalized * (minPushForce + (maxPushForce - minPushForce) * (forceDirection.magnitude / range)), ForceMode2D.Impulse);
            }
        }
        Instantiate(fx, transform.position, transform.rotation, LevelContainer.instance.transform);
        PlayerController.instance.RepulseProjectiles();
    }
}
