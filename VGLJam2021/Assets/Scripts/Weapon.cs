using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootType
{
    SemiAuto,
    Auto,
}

public class Weapon : MonoBehaviour
{
    public Transform projectilePrefab;
    public Transform spawnPoint;
    public ShootType shootType;
    public float shootInterval = 0.3f;
    private float shootTime = 0;
    public float precision = 10;
    public Team team;
    public LayerMask collisionLayer;
    public bool isPlayer = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(!isPlayer) return;
        
        switch(shootType)
        {
            case ShootType.SemiAuto:
                if(Input.GetButtonDown("Shoot") && shootTime < 0)
                {
                    shootTime = shootInterval;
                    Shoot();

                }
                break;

            case ShootType.Auto:
                if(Input.GetButton("Shoot") && shootTime < 0)
                {
                    shootTime = shootInterval;
                    Shoot();
                }
                break;

        }
        shootTime -= Time.deltaTime;
    }

    public void Shoot()
    {
        if(projectilePrefab != null)
        {
            Vector3 direction = spawnPoint.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude, collisionLayer);
            Vector3 spawnPosition = spawnPoint.position;
            if(hit.collider != null)
                spawnPosition = transform.position;
            Transform projectile = Instantiate(projectilePrefab, spawnPosition, transform.rotation * Quaternion.AngleAxis(RandomGaussian(0.2f, 0) * precision, Vector3.forward), LevelContainer.instance.transform);
            projectile.gameObject.AddComponent<TeamDataHolder>().team = team;
        }
    }

     public float RandomGaussian(float sigma, float mu)
    {
        float x1, x2, w, y1; //, y2;

        do
        {
            x1 = 2f * (float)Random.value - 1f;
            x2 = 2f * (float)Random.value - 1f;
            w = x1 * x1 + x2 * x2;
        } while (w >= 1f);

        w = Mathf.Sqrt((-2f * Mathf.Log(w)) / w);
        y1 = x1 * w;
        // y2 = x2 * w;

        return (y1 * sigma) + mu;
    }
}
