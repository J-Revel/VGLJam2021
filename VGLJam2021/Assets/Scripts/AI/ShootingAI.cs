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
    public float loadDuration = 2;
    private float loadTime = 0;
    public float shootTime = 0;
    public float angularAcceleration = 10;
    public int burstCount = 5;
    private int burstIndex = 0;
    public float reloadDuration = 2;
    private float reloadTime = 0;
    private bool loading = false;
    public float shootInterval = 0.5f;

    public Weapon weapon;
    public GameObject loadingFX;

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
        if(loading)
        {
            if(loadTime < loadDuration)
            {
                loadTime += Time.deltaTime;
                print(loadTime);
                if(loadTime >= loadDuration)
                    loadingFX.SetActive(false);
                
            }
            else
            {
                if(burstIndex >= burstCount)
                {
                    loading = false;
                }
                else
                {
                    shootTime += Time.deltaTime;
                    if(shootTime > shootInterval)
                    {
                        weapon.Shoot();
                        burstIndex++;
                        shootTime -= shootInterval;
                    }
                }
            }

        }
        if(targetDistance > shootRange * shootRange)
            rigidbody.AddForce(movementForce * targetDirection.normalized);
        else if(targetDistance < escapeRange * escapeRange)
            rigidbody.AddForce(-escapeForce * targetDirection.normalized);
        else
        {
            
            if(burstIndex == 0 && !loading)
            {
                loading = true;
                loadingFX.SetActive(true);
            }
        }
        if(burstIndex >= burstCount)
        {
            loading = false;
            reloadTime += Time.deltaTime;
            if(reloadTime > reloadDuration)
            {
                reloadTime = 0;
                burstIndex = 0;
                loadTime = 0;
            }
        }
        rigidbody.AddTorque(Mathf.Sign(Vector2.SignedAngle(transform.right, targetDirection)) * angularAcceleration);
    }
}
