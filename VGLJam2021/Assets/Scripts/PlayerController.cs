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

    public float targetDistanceRatio = 0.5f;
    public float maxTargetDistance = 5;

    public static PlayerController instance;
    public AnimatedSprite animatedSprite;

    void Awake()
    {
        instance = this;
    }
    
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
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            animatedSprite.SelectAnim("Walk");
        }
        else animatedSprite.SelectAnim("Idle");
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
        Camera camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(-ray.origin.z / Vector3.Dot(ray.direction, new Vector3(0, 0, 1)));
        print(targetPosition);
        float angle = Vector2.SignedAngle(Vector3.right, targetPosition-transform.position);
        weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float targetDistance = Vector3.Distance(targetPosition, transform.position);
        Vector3 targetDirection = (targetPosition-transform.position).normalized;
        cameraTarget.transform.position = transform.position + targetDirection * Mathf.Min(maxTargetDistance, targetDistance * targetDistanceRatio);
    }
}
