using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1;
    public float dashDistance = 4;
    public new Rigidbody2D rigidbody;
    public LayerMask raycastLayer;
    public Transform cameraTarget;
    public Transform weaponTransform;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = (Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical")) * movementSpeed;
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Dash"))
        {
            Vector2 inputDirection = ((Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical")).normalized);
            RaycastHit2D hit = Physics2D.Raycast(rigidbody.position, inputDirection, dashDistance, raycastLayer);
            if(hit && hit.distance > 0)
            {
                rigidbody.MovePosition(rigidbody.position + inputDirection * hit.distance);
            }
            else
                rigidbody.MovePosition(rigidbody.position + inputDirection * dashDistance);
        }
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Vector2.SignedAngle(Vector3.right, targetPosition-transform.position);
        weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
