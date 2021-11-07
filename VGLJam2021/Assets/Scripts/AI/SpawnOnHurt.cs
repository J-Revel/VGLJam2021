using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnHurt : MonoBehaviour
{
    public Transform hurtSpawn;
    public Transform deathSpawn;
    private void Start()
    {
        Health health = GetComponent<Health>();
        health.hurtDelegate += OnHurt;
        health.deathDelegate += OnDeath;
    }

    private void OnHurt()
    {
        Instantiate(hurtSpawn, transform.position, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
    }

    private void OnDeath(Vector2 direction)
    {
        Instantiate(deathSpawn, transform.position, Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.right, direction), Vector3.forward));
    }
}
