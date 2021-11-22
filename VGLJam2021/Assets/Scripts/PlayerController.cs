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
    public Transform dashFXPrefab;
    public int dashFXCount = 5;
    public AudioSource dashAudioSource;
    public float projectileRepulsionDuration = 0.3f;
    public float projectileRepulsionRange = 0.3f;
    private float projectileRepulsionTime = 0;

    private string vAxis;
    private string hAxis;
    

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        vAxis = TranslateService.instance.Translate("axis_v");
        hAxis = TranslateService.instance.Translate("axis_h");
    }

    private void OnLanguageChange()
    {
        vAxis = TranslateService.instance.Translate("axis_v");
        hAxis = TranslateService.instance.Translate("axis_h");
    }

    void FixedUpdate()
    {
        rigidbody.velocity = (Vector3.right * Input.GetAxis(hAxis) + Vector3.up * Input.GetAxis(vAxis)) * movementSpeed;
        
    }

    public bool projectileRepulsionActive { get { return projectileRepulsionTime > 0; } }

    public void RepulseProjectiles() 
    {
        projectileRepulsionTime = projectileRepulsionDuration;
    }

    void Update()
    {
        MusicPlayer.instance.transform.position = transform.position;
        if(Mathf.Abs(Input.GetAxis(hAxis)) > 0.1f || Mathf.Abs(Input.GetAxis(vAxis)) > 0.1f)
        {
            animatedSprite.SelectAnim("Walk");
        }
        else animatedSprite.SelectAnim("Idle");
        if(Input.GetButtonDown("Dash"))
        {
            dashAudioSource.Play();
            Vector2 inputDirection = ((Vector3.right * Input.GetAxis(hAxis) + Vector3.up * Input.GetAxis(vAxis)).normalized);
            RaycastHit2D hit = Physics2D.Raycast(rigidbody.position, inputDirection, dashDistance, raycastLayer);
            Vector2 dashDirection = Vector3.zero;
            if(hit && hit.distance > 0)
            {
                dashDirection =  inputDirection * hit.distance;
            }
            else
                dashDirection = inputDirection * dashDistance;
            rigidbody.MovePosition(rigidbody.position + dashDirection);
            for(int i=0; i<dashFXCount; i++)
            {
                SpriteRenderer spriteRenderer = Instantiate(dashFXPrefab, transform.position + new Vector3(dashDirection.x, dashDirection.y, 0) * i / dashFXCount, Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.color = new Color(1, 1, 1, (float)i / dashFXCount);
                spriteRenderer.flipX = rigidbody.velocity.x < 0;
            }
        }
        Camera camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(-ray.origin.z / Vector3.Dot(ray.direction, new Vector3(0, 0, 1)));
        float angle = Vector2.SignedAngle(Vector3.right, targetPosition-transform.position);
        weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float targetDistance = Vector3.Distance(targetPosition, transform.position);
        Vector3 targetDirection = (targetPosition-transform.position).normalized;
        cameraTarget.transform.position = transform.position + targetDirection * Mathf.Min(maxTargetDistance, targetDistance * targetDistanceRatio);
        if(projectileRepulsionTime > 0)
        {
            projectileRepulsionTime -= Time.deltaTime;
        }
    }
}
