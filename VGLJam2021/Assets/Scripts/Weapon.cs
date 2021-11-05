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
    public ShootType shootType;
    public float shootInterval = 0.3f;
    private float shootTime = 0;
    public float precision = 10;

    void Start()
    {
        
    }

    void Update()
    {
        switch(shootType)
        {
            case ShootType.SemiAuto:
                if(Input.GetButtonDown("Shoot") && shootTime < 0)
                {
                    shootTime = shootInterval;
                    Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.AngleAxis(Random.Range(-precision, precision), Vector3.forward));
                }
                break;

            case ShootType.Auto:
                if(Input.GetButton("Shoot") && shootTime < 0)
                {
                    shootTime = shootInterval;
                    Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.AngleAxis(Random.Range(-precision, precision), Vector3.forward));
                }
                break;

        }
        shootTime -= Time.deltaTime;
    }
}
