using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    private new Rigidbody2D rigidbody;
    public float force = 50;

    void Start()
    {
        targetOffset = transform.position - target.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody.AddForce((target.position - transform.position) * force);
    }

    void FixedUpdate()
    {
    }
}
