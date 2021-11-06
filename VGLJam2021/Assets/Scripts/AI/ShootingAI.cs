using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAI : MonoBehaviour
{
    private new Rigidbody2D rigidbody; 
    public float movementForce = 10;
    public float escapeForce = 5;
    public float shootRange = 10;
    public float escapeRange = 5;
    public float shootInterval = 2;
    public float shootTime = 0;
    public float angularAcceleration = 10;

    public Weapon weapon;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Vector3 targetPosition = PlayerController.instance.transform.position;
        Vector3 targetDirection = targetPosition - transform.position;
        float targetDistance = targetDirection.sqrMagnitude;
        float angle = Vector2.SignedAngle(Vector3.right, targetPosition-transform.position);
        // weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if(targetDistance > shootRange * shootRange)
            rigidbody.AddForce(movementForce * targetDirection.normalized);
        else if(targetDistance < escapeRange * escapeRange)
            rigidbody.AddForce(-escapeForce * targetDirection.normalized);
        else
        {
            shootTime += Time.deltaTime;
            if(shootTime > shootInterval)
            {
                weapon.Shoot();
                shootTime -= shootInterval;
            }
        }
        rigidbody.AddTorque(Mathf.Sign(Vector2.SignedAngle(transform.right, targetDirection)) * angularAcceleration);
    }
}
